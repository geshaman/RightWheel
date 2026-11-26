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
        public async Task<IActionResult> GetAll()
        {
            var parts = await _db.Parts
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.Quantity,
                    p.Description,
                    p.Position,
                    p.Images,
                    p.Categories,
                    p.Cars
                })
                .ToListAsync();
            return Ok(parts);
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
                    p.Categories,
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
