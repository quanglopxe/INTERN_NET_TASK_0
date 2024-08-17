using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;

namespace XuongMay_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MyDbContext _context;
        public CategoryController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var listCategory = _context.Categories.ToList();
            return Ok(listCategory);
        }
        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            var category = _context.Categories.FirstOrDefault(ca => ca.CategoryID == id);
            if(category != null)
            {
                return Ok(category);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult Create(CategoryModel model)
        {
            try
            {
                var category = new Category()
                {
                    CategoryName = model.CategoryName
                };
                _context.Add(category);
                _context.SaveChanges();
                return Ok(category);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateByID(Guid id, CategoryModel model)
        {
            var category = _context.Categories.FirstOrDefault(ca => ca.CategoryID == id);
            if (category != null)
            {
                category.CategoryName = model.CategoryName;
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
