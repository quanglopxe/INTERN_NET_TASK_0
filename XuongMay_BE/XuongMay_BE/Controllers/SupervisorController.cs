using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XuongMay_BE.Data;
using XuongMay_BE.Models;

namespace XuongMay_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupervisorController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SupervisorController(MyDbContext context)
        {
            _context = context;
        }

        // API GET để tìm toàn bộ Supervisor 
        [HttpGet]
        public IActionResult GetAll()
        {
            var listSupervisor = _context.Supervisors.ToList();
            return Ok(listSupervisor);
        }

        // API POST để tạo Supervisor mới
        [HttpPost]
        public IActionResult Create(SupervisorModel model)
        {
            try
            {
                var supervisor = new Supervisor()
                {
                    SupervisorName = model.SupervisorName,                    
                };
                _context.Supervisors.Add(supervisor);
                _context.SaveChanges();
                return Ok(supervisor);
            }
            catch
            {
                return BadRequest();
            }
        }

        // API GET để tìm Supervisor theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var supervisor = await _context.Supervisors
                .FirstOrDefaultAsync(s => s.SupervisorID == id);

            if (supervisor == null)
            {
                return NotFound();
            }

            return Ok(supervisor);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupervisor(Guid id)
        {
            // Tìm kiếm supervisor theo ID
            var supervisor = await _context.Supervisors.FindAsync(id);

            if (supervisor == null)
            {
                return NotFound();
            }

            // Xóa supervisor
            _context.Supervisors.Remove(supervisor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupervisor(Guid id, Supervisor supervisor)
        {
            // Kiểm tra xem đối tượng cần cập nhật có tồn tại không
            if (id != supervisor.SupervisorID)
            {
                return BadRequest();
            }

            _context.Entry(supervisor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupervisorExists(id))
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

        // Kiểm tra xem Supervisor có tồn tại không
        private bool SupervisorExists(Guid id)
        {
            return _context.Supervisors.Any(e => e.SupervisorID == id);
        }

    }
}
