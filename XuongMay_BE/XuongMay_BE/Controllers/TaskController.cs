using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;
using Microsoft.AspNetCore.Authorization;


namespace XuongMay_BE.Controllers
{
    [Authorize(Roles = "User")]
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
            var listTask = _context.Tasks.ToList();
            return Ok(listTask);
        }
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
                        var task = new Data.Task()
                        {
                            OrderID = model.OrderID,
                            StageID = model.StageID,
                            EmpID = model.EmpID,
                            SupervisorID = Guid.Parse(userID),
                            Status = model.Status,
                            StartTime = model.StartTime,
                            EndTime = model.EndTime,
                            Remarks = model.Remarks,
                        };
                        _context.Add(task);
                        _context.SaveChanges();
                        return Ok(task);
                    }
                }
            }
            catch
            {
                return BadRequest();
            }
        }
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
                        task.SupervisorID = Guid.Parse(userID);
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
    }
}
