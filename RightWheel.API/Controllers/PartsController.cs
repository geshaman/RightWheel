using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RightWheel.Infrastructure;

namespace RightWheel.API.Controllers
{
    [ApiController]
    [Route("api/parts")]
    public class PartsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PartsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            if (pageSize > 50 || page < 1 || pageSize <= 0) return BadRequest();
            var query = _db.Parts
                .AsNoTracking()
                .Where(p => p.Quantity > 0);
            var total = await query.CountAsync();
            var items = await query
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.Quantity,
                    p.Description,
                    p.Position,
                    Images = p.Images
                        .Select(i => i.ImageUrl)
                        .ToList(),
                    Categories = p.Categories
                        .Select(c => new 
                        {
                            c.Id,
                            c.Name,
                            c.ParentCategoryId
                        })
                        .ToList(),
                    Cars = p.Cars
                        .Select(c => new
                        {
                            c.Id,
                            c.Brand,
                            c.Model,
                            c.Engine,
                        })
                        .ToList()
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return Ok(new {items, total});
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var part = await _db.Parts
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.Quantity,
                    p.Description,
                    p.Position,
                    Images = p.Images
                        .Select(i => i.ImageUrl)
                        .ToList(),
                    Categories = p.Categories
                        .Select(c => new
                        {
                            c.Id,
                            c.Name,
                            c.ParentCategoryId
                        })
                        .ToList(),
                    Cars = p.Cars
                        .Select(c => new
                        {
                            c.Id,
                            c.Brand,
                            c.Model,
                            c.Engine,
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
            if (part is null) return NotFound();
            return Ok(part);
        }
    }
}
