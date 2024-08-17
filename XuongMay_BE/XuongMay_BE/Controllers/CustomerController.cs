using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            var listCustomer = _context.Customers.ToList();
            return Ok(listCustomer);
        }

        // API POST để tạo khách hàng mới
        [HttpPost]
        public IActionResult Create(CustomerModel model)
        {
            try
            {
                var customer = new Customer()
                {
                    CustomerID = model.CustomerID,
                    CustomerName = model.CustomerName,
                    Phone = model.Phone,
                    Address = model.Address
                };
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return Ok(customer);
            }
            catch
            {
                return BadRequest();
            }
        }

        // API GET để tìm khách hàng theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {      
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerID == id);
            if (customer == null)
            {
                return NotFound(); 
            }

            return Ok(customer); 
        }
    }
}
