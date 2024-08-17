using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XuongMay_BE.Data;
using XuongMay_BE.Models;

namespace XuongMay_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionLineController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ProductionLineController(MyDbContext context)
        {
            _context = context;
        }

        // API GET để tìm toàn bộ ProductionLine 
        [HttpGet]
        public IActionResult GetAll()
        {
            var listProductionLine = _context.ProductionLines.ToList();
            return Ok(listProductionLine);
        }

        // API PoST để tạo ProductionLine mới
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
                return Ok(productionLine);
            }
            catch
            {
                return BadRequest();
            }
        }

        // API GET để tìm ProductionLine theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var productionLine = await _context.ProductionLines
                .FirstOrDefaultAsync(pl => pl.LineID == id);

            if (productionLine == null)
            {
                return NotFound();
            }

            return Ok(productionLine);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductionLine(int id)
        {
            // Tìm kiếm production line theo ID
            var productionLine = await _context.ProductionLines.FindAsync(id);

            if (productionLine == null)
            {
                return NotFound();
            }

            // Xóa production line
            _context.ProductionLines.Remove(productionLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductionLine(int id, ProductionLine productionLine)
        {
            // Kiểm tra xem đối tượng cần cập nhật có tồn tại không
            if (id != productionLine.LineID)
            {
                return BadRequest();
            }

            _context.Entry(productionLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductionLineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Kiểm tra xem Production Line có tồn tại không
        private bool ProductionLineExists(int id)
        {
            return _context.ProductionLines.Any(e => e.LineID == id);
        }

    }
}
