using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                var listCategory = _context.Categories.ToList();
                return Ok(listCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving categories: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(ca => ca.CategoryID == id);
                if (category != null)
                {
                    return Ok(category);
                }
                else
                {
                    return NotFound($"Category with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving the category: {ex.Message}");
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
                return CreatedAtAction(nameof(GetByID), new { id = category.CategoryID }, category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating the category: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateByID(Guid id, CategoryModel model)
        {
            try
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
                    return NotFound($"Category with ID {id} not found.");
                }
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while updating the category: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
            // Tìm kiếm category theo ID
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            // Xóa customer
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
                 return Ok("Category deleted successfully.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while deleting the category");

            }
        }
    }
}
