using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XuongMay_BE.Data;
using XuongMay_BE.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace XuongMay_BE.Controllers
{
    [Authorize(Roles = "Supervisor, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionLineController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ProductionLineController(MyDbContext context)
        {
            _context = context;
        }

        // API GET để lấy toàn bộ ProductionLine 
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var listProductionLine = _context.ProductionLines.ToList();
                return Ok(listProductionLine);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving production lines.");
            }
        }

        // API POST để tạo ProductionLine mới
        [HttpPost]
        public IActionResult Create(ProductionLineModel model)
        {
            try
            {
                var productionLine = new ProductionLine()
                {
                    LineName = model.LineName,
                    SupervisorID = model.SupervisorID
                };
                _context.ProductionLines.Add(productionLine);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, productionLine); 
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the production line.");
            }
        }

        // API GET để tìm ProductionLine theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var productionLine = await _context.ProductionLines
                    .FirstOrDefaultAsync(pl => pl.LineID == id);

                if (productionLine == null)
                {
                    return NotFound($"Production line with ID {id} not found.");
                }

                return Ok(productionLine);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the production line.");
            }
        }

        // API DELETE để xóa ProductionLine theo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductionLine(Guid id)
        {
            try
            {
                var productionLine = await _context.ProductionLines.FindAsync(id);

                if (productionLine == null)
                {
                    return NotFound($"Production line with ID {id} not found."); // 404 Not Found
                }

                _context.ProductionLines.Remove(productionLine);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the production line.");
            }
        }

        // API PUT để cập nhật ProductionLine theo ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductionLine(Guid id, ProductionLineModel model)
        {
            try
            {
                var productionLine = await _context.ProductionLines.FirstOrDefaultAsync(pl => pl.LineID == id);

                if (productionLine == null)
                {
                    return NotFound($"Production line with ID {id} not found."); 
                }

                productionLine.LineName = model.LineName;
                productionLine.SupervisorID = model.SupervisorID;

                _context.Entry(productionLine).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent(); 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductionLineExists(id))
                {
                    return NotFound($"Production line with ID {id} not found."); 
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "A concurrency error occurred while updating the production line.");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the production line.");
            }
        }

        // Kiểm tra xem Production Line có tồn tại không
        private bool ProductionLineExists(Guid id)
        {
            return _context.ProductionLines.Any(e => e.LineID == id);
        }
    }
}
