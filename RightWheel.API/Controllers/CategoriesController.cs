using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RightWheel.Infrastructure;

namespace RightWheel.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        public readonly AppDbContext _db;

        public CategoriesController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _db.Categories
                .AsNoTracking()
                .Select(c => new { c.Id, c.Name, c.ParentCategoryId })
                .ToListAsync();
            return Ok(categories);
        }
    }
}
