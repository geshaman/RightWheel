using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RightWheel.Application.DTO.Request;
using RightWheel.Domain.Models;
using RightWheel.Infrastructure;

namespace RightWheel.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public OrdersController(AppDbContext db) {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(
            [FromBody] OrderRequest request)
        {
            var requested = request.Items
                .GroupBy(i => i.PartId)
                .ToDictionary(g => g.Key, g => g.Sum(i => i.PartQuantity));
            var parts = await _db.Parts
                .AsNoTracking()
                .Where(p => requested.Keys.Contains(p.Id))
                .Select(p => new { p.Id, p.Name, p.Price })
                .ToDictionaryAsync(p => p.Id);
            if (parts.Count != requested.Count) return BadRequest(new { message = "Какой-то из деталей не существует" });
            var items = new List<OrderItem>();
            foreach (var item in requested)
            {
                items.Add(new OrderItem
                {
                    PartId = item.Key,
                    PartQuantity = item.Value,
                    PriceAtOrder = parts[item.Key].Price
                });
            }

            await using var tx = await _db.Database.BeginTransactionAsync();
            foreach (var item in requested)
            {
                var affected = await _db.Parts
                    .Where(p => p.Id == item.Key && p.Quantity >= item.Value)
                    .ExecuteUpdateAsync(s => s.SetProperty(p => p.Quantity, p => p.Quantity - item.Value));
                if (affected == 0) return Conflict(new {message = $"Ошибка: детали {parts[item.Key].Name} не хватает для оформления заказа"});
            }
            var now = DateTime.UtcNow;
            var order = new Order
            {
                ClientName = request.ClientName,
                ClientPhoneNumber = request.ClientPhoneNumber,
                OrderCreateTime = now,
                OrderEndTime = now.AddDays(3),
                Items = items,
                Status = Status.Pending,
                VIN = request.VIN
            };

            _db.Add(order);
            await _db.SaveChangesAsync();
            await tx.CommitAsync();
            return Created(string.Empty, new {order.Id});
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> CancelOrder(long id)
        {
            var order = await _db.Orders
                .AsNoTracking()
                .Where(o => o.Id == id)
                .Include(o => o.Items)
                .FirstOrDefaultAsync();
            if (order is null) return NotFound();

            await using var tx = await _db.Database.BeginTransactionAsync();
            var affected = await _db.Orders
                     .Where(o => o.Id == id && o.Status == Status.Pending)
                     .ExecuteUpdateAsync(s => s.SetProperty(o => o.Status, Status.Canceled));
            if (affected == 0) return Conflict(new { message = "Заявку нельзя отменить, т.к. она уже подтверждена, обработана или отменена." });
            foreach (var item in order.Items)
            {   
                await _db.Parts
                    .Where(p => p.Id == item.PartId)
                    .ExecuteUpdateAsync(s => s.SetProperty(p => p.Quantity, p => p.Quantity + item.PartQuantity));
            }

            await tx.CommitAsync();
            return NoContent();
        }
    }
}
