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
        //[Authorize(Roles = AppRole.Admin)]
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
        //[Authorize(Roles = AppRole.Admin)]
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

    }
}
