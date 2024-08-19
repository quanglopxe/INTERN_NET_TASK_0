using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace XuongMay_BE.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CustomerController(MyDbContext context)
        {
            _context = context;
        }

        // API GET để lấy tất cả khách hàng
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var listCustomer = _context.Customers.ToList();
                return Ok(listCustomer);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving customers.");
            }
        }

        // API POST để tạo khách hàng mới
        [HttpPost]
        public IActionResult Create(CustomerModel model)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid customer data."); 
                }

                var customer = new Customer()
                {
                    CustomerID = model.CustomerID,
                    CustomerName = model.CustomerName,
                    Phone = model.Phone,
                    Address = model.Address
                };
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, customer);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the customer.");
            }
        }

        // API GET để tìm khách hàng theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == id);

                if (customer == null)
                {
                    return NotFound($"Customer with ID {id} not found.");
                }

                return Ok(customer); 
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the customer.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);

                if (customer == null)
                {
                    return NotFound($"Customer with ID {id} not found."); 
                }
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return Ok("Customer deleted successfully."); 
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the customer.");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, CustomerModel model)
        {
            try
            {
                // Tìm kiếm Customer theo ID
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == id);
                if (customer == null)
                {
                    return NotFound($"Customer with ID {id} not found."); // 404 Not Found
                }

                // Cập nhật thông tin Customer
                customer.CustomerName = model.CustomerName;
                customer.Phone = model.Phone;
                customer.Address = model.Address;

                
                await _context.SaveChangesAsync();

                return NoContent(); 
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A concurrency error occurred while updating the customer.");
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the customer.");
            }
        }



        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    }
}
