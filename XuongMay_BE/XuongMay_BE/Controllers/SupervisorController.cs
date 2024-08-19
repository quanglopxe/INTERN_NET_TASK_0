using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XuongMay_BE.Data;
using XuongMay_BE.Models;

namespace XuongMay_BE.Controllers
{
    [Authorize(Roles = "Admin")]
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
            try
            {
                var listSupervisor = _context.Supervisors.ToList();
                return Ok(listSupervisor);

            }catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving data.");
            }
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
                return StatusCode(StatusCodes.Status201Created, supervisor);
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
                return NotFound($"Supervisor with ID {id} not found.");
            }

            return Ok(supervisor);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupervisor(Guid id)
        {
            try
            {
                var supervisor = await _context.Supervisors.FindAsync(id);

                if (supervisor == null)
                {
                    return NotFound($"Supervisor with ID {id} not found."); 
                }

                _context.Supervisors.Remove(supervisor);
                await _context.SaveChangesAsync();

                return Ok("Supervisor deleted successfully."); 
            }
            catch 
            {
                // Log exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the supervisor.");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupervisor(Guid id, SupervisorModel model)
        {
            try
            {
                // Tìm kiếm Supervisor theo ID
                var supervisor = await _context.Supervisors.FirstOrDefaultAsync(s => s.SupervisorID == id);
                if (supervisor == null)
                {
                    return NotFound($"Supervisor with ID {id} not found."); 
                }

                // Cập nhật thông tin Supervisor
                supervisor.SupervisorName = model.SupervisorName;
                supervisor.LineID = model.LineID;

                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();

                return NoContent(); 
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A concurrency error occurred while updating the supervisor.");
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the supervisor.");
            }


        }

        // Kiểm tra xem Supervisor có tồn tại không
        private bool SupervisorExists(Guid id)
        {
            return _context.Supervisors.Any(e => e.SupervisorID == id);
        }

    }
}
