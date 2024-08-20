using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetById(Guid id)
        {      
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerID == id);
            if (customer == null)
            {
                return NotFound(); 
            }

            return Ok(customer); 
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            // Tìm kiếm customer theo ID
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            // Xóa customer
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, Customer customer)
        {
            // Kiểm tra xem ID từ URL có khớp với ID trong body không
            if (id != customer.CustomerID)
            {
                return BadRequest("ID mismatch");
            }

            // Kiểm tra tính hợp lệ của mô hình
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpGet("api/[controller]")]
        public async Task<IActionResult> PagCustomer(int page = 1, int pageSize = 10)
        {
            var totalItems = await _context.Customers.CountAsync();
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

            var cus = await _context.Customers
                .Skip((page - 1) * pageSize) 
                .Take(pageSize)
                .ToListAsync();

            var result = new
            {
                data = cus,
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

        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    }
}
