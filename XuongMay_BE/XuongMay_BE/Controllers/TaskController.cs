using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        //API lấy tất cả danh sách Task
        [HttpGet]
        public IActionResult GetAll()
        {
            var listTask = _context.Tasks.ToList();
            return Ok(listTask);
        }
        //API lấy Task từ ID
        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            var task = _context.Tasks.FirstOrDefault(ca => ca.TaskID == id);
            if (task != null)
            {
                return Ok(task);
            }
            else
            {
                return NotFound();
            }
        }
        //Kiểm tra quyền Supervisor mới được thêm Task
        [Authorize(Roles = "Supervisor")]
        [HttpPost]
        public IActionResult Create(TaskModel model)
        {
            try
            {
                //Get UserID from session
                var userID = HttpContext.Session.GetString("UserId");

                if (string.IsNullOrEmpty(userID))
                    return Unauthorized("Vui lòng đăng nhập trước khi tạo Order Detail.");
                else
                {
                    //Check if the user is Supervisor
                    var User = _context.Users.Find(Guid.Parse(userID));
                    if (User.Role != UserRole.Supervisor)
                        return Unauthorized("Chỉ có quyền truy cập của Supervisor mới có thể tạo Order Detail.");
                    else
                    {
                        var Supervisor = _context.Supervisors.FirstOrDefault(s => s.UserID == Guid.Parse(userID));
                        var SupID = Guid.Empty;
                        if (Supervisor != null)
                            SupID = Supervisor.SupervisorID;
                        var task = new Data.Task()
                        {
                            TaskID = Guid.NewGuid(),
                            OrderID = model.OrderID,
                            StageID = model.StageID,
                            EmpID = model.EmpID,
                            SupervisorID = SupID,
                            Status = model.Status,
                            StartTime = model.StartTime,
                            EndTime = model.EndTime,
                            Remarks = model.Remarks,
                        };
                        // Configure JsonSerializerOptions to handle reference loops
                        var options = new JsonSerializerOptions
                        {
                            ReferenceHandler = ReferenceHandler.IgnoreCycles,
                            WriteIndented = true
                        };

                        _context.Add(task);
                        _context.SaveChanges();
                        // Serialize the task using the configured options
                        var taskJson = JsonSerializer.Serialize(task, options);
                        return Ok(taskJson);
                    }
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        //Kiểm tra quyền Supervisor mới được sửa Task
        [Authorize(Roles = "Supervisor")]
        [HttpPut("{id}")]
        public IActionResult UpdateByID(Guid id, TaskModel model)
        {
            //Get UserID from session
            var userID = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userID))
                return Unauthorized("Vui lòng đăng nhập trước khi tạo Order Detail.");
            else
            {
                //Check if the user is Supervisor
                var User = _context.Users.Find(Guid.Parse(userID));
                if (User.Role != UserRole.Supervisor)
                    return Unauthorized("Chỉ có quyền truy cập của Supervisor mới có thể tạo Order Detail.");
                else
                {
                    var task = _context.Tasks.FirstOrDefault(ca => ca.TaskID == id);
                    if (task != null)
                    {
                        task.OrderID = model.OrderID;
                        task.StageID = model.StageID;
                        task.EmpID = model.EmpID;
                        //task.SupervisorID = Guid.Parse(userID);
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
            }
            
        }
        //Kiểm tra quyền Supervisor mới được xóa Task
        [Authorize(Roles = "Supervisor")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Tìm kiếm task theo ID
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            // Xóa customer
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //API lấy danh sách Task theo trang
        [HttpGet("api/[controller]")]
        public async Task<IActionResult> PagTask(int page = 1, int pageSize = 10)
        {
            var totalItems = await _context.Tasks.CountAsync();
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

            var task = await _context.Tasks
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
