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
            try
            {
                var lstEmp = _context.Employees.ToList();
                return Ok(lstEmp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving employees: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            try
            {
                var employee = _context.Employees.FirstOrDefault(emp => emp.EmpID == id);

                if (employee == null)
                {
                    return NotFound($"Employee with ID {id} not found.");
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving the employee: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult createEmp(EmployeeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var emp = new Employee()
                {
                    EmpName = model.EmpName
                };
                _context.Add(emp);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetByID), new { id = emp.EmpID }, emp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating the employee: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateByID(Guid id, EmployeeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var employee = _context.Employees.FirstOrDefault(emp => emp.EmpID == id);

                if (employee == null)
                {
                    return NotFound($"Employee with ID {id} not found.");
                }

                employee.EmpName = model.EmpName;
                _context.Entry(employee).State = EntityState.Modified;
                _context.SaveChanges();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status409Conflict, $"A concurrency error occurred while updating the employee with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while updating the employee: {ex.Message}");
            }
        }

    }
}
