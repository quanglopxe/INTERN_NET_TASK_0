using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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
            if (category != null)
            {
                return Ok(category);
            }
            else
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Tìm kiếm category theo ID
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            // Xóa customer
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("api/[controller]")]
        public async Task<IActionResult> PagCategory(int page = 1, int pageSize = 10)
        {
            var totalItems = await _context.Categories.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Đảm bảo không vượt quá số trang tối đa
            if (page > totalPages)
            {
                page = totalPages;
            }

            // trường hợp không có sản phẩm
            if (totalPages == 0)
            {
                page = 1;
                totalPages = 1;
            }

            var cat = await _context.Categories
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new
            {
                data = cat,
                pagination = new
                {
                    currentPage = page,
                    totalPages = totalPages,
                    totalItems = totalItems,
                    itemsPerPage = pageSize
                }
            };

            return Ok(result);
        }
    }
}
