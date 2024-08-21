using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XuongMay_BE.Data;
using XuongMay_BE.Models;
using Microsoft.AspNetCore.Authorization;



namespace XuongMay_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : ControllerBase
    {
        private readonly MyDbContext _context;
        public EmployeeController(MyDbContext context) { _context = context; }

        [HttpGet]
        public IActionResult getAll()
        {
            var lstEmp = _context.Employees.ToList();
            return Ok(lstEmp);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            var employee = _context.Employees.FirstOrDefault(emp => emp.EmpID == id);
            if (employee != null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult createEmp(EmployeeModel model)
        {
            try
            {
                var emp = new Employee()
                {
                    EmpName = model.EmpName
                };
                _context.Add(emp);
                _context.SaveChanges();
                return Ok(emp);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateByID(Guid id, EmployeeModel model)
        {
            var employee = _context.Employees.FirstOrDefault(emp => emp.EmpID == id);
            if (employee != null)
            {
                employee.EmpName = model.EmpName;
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("api/[controller]")]
        public async Task<IActionResult> PagEmployee(int page = 1, int pageSize = 10)
        {
            var totalItems = await _context.Employees.CountAsync(); // Đếm tổng số sản phẩm
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize); // Tính tổng số trang

            if (page > totalPages)
            {
                page = totalPages;
            }

            if (totalPages == 0)
            {
                page = 1;
                totalPages = 1;
            }

            var emp = await _context.Employees
                .Skip((page - 1) * pageSize)  // Bỏ qua các mục không thuộc trang hiện tại
                .Take(pageSize)               // Lấy số lượng mục trên mỗi trang
                .ToListAsync();

            var result = new
            {
                data = emp,
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
