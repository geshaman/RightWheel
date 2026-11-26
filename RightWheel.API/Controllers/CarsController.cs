using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RightWheel.Infrastructure;

namespace RightWheel.API.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CarsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _db.Cars
                .AsNoTracking()
                .OrderBy(c => c.Brand)
                .ThenBy(c => c.Model)
                .Select(c => new {c.Id, c.Brand, c.Model, c.Engine})
                .ToListAsync();
            return Ok(cars);
        }
    }
}
