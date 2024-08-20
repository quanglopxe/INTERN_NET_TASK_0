using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XuongMay_BE.Data;
using XuongMay_BE.Models;

namespace XuongMay_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly MyDbContext _context;
        public TaskController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var listTask = _context.Task.ToList();
            return Ok(listTask);
        }
        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            var task = _context.Task.FirstOrDefault(ca => ca.TaskID == id);
            if (task != null)
            {
                return Ok(task);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult Create(TaskModel model)
        {
            try
            {
                var task = new Data.Task()
                {
                    OrderID = model.OrderID,
                    StageID = model.StageID,
                    AssignedTo = model.AssignedTo,
                    AssignedBy = model.AssignedBy,
                    Status = model.Status,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    Remarks = model.Remarks,
                };
                _context.Add(task);
                _context.SaveChanges();
                return Ok(task);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateByID(Guid id, TaskModel model)
        {
            var task = _context.Task.FirstOrDefault(ca => ca.TaskID == id);
            if (task != null)
            {
                task.OrderID = model.OrderID;
                task.StageID = model.StageID;
                task.AssignedTo = model.AssignedTo;
                task.AssignedBy = model.AssignedBy;
                task.Status = model.Status;
                task.StartTime = model.StartTime;
                task.EndTime = model.EndTime;
                task.Remarks = model.Remarks;

                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Tìm kiếm task theo ID
            var task = await _context.Task.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            // Xóa customer
            _context.Task.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("api/[controller]")]
        public async Task<IActionResult> PagTask(int page = 1, int pageSize = 10)
        {
            var totalItems = await _context.Task.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            if (page > totalPages)
            {
                page = totalPages;
            }

            if (totalPages == 0)
            {
                page = 1;
                totalPages = 1;
            }

            var task = await _context.Task
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new
            {
                data = task,
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
