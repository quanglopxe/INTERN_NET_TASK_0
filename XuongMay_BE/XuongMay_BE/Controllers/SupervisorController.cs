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
                    LineID = model.LineID
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
        public async Task<IActionResult> GetById(int id)
        {
            var supervisor = await _context.Supervisors
                .FirstOrDefaultAsync(s => s.SupervisorID == id);

            if (supervisor == null)
            {
                return NotFound();
            }

            return Ok(supervisor);
        }
    }
}
