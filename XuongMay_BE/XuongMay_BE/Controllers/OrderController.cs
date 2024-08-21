using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace XuongMay_BE.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MyDbContext _context;
        public OrdersController(MyDbContext context)
        {
            _context = context;
        }


        //Lấy tất cả danh sách Orders
        [HttpGet]
        public IActionResult GetAll()
        {
            var dsOrder = _context.Orders.ToList();
            return Ok(dsOrder);
        }


        //Lấy Order từ ID
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            //Lấy Order từ ID được nhập
            var orderss = _context.Orders.SingleOrDefault(s => s.OrderID == id);
            //Kiểm tra orderss có được gán giá trị vào hay không?
            if (orderss != null)
            {
                return Ok(orderss);
            }
            else
            {
                return NotFound();
            }
        }


        //Thêm mới Order
        [HttpPost]
        public IActionResult CreateOrder(OrderModel orders)
        {
            try
            {
                //Gán giá trị nhập vào từng thuộc tính của order
                var orderss = new Orders
                {
                    OrderDate = orders.OrderDate,
                    CustomerID = orders.CustomerID,
                    DeliveryDate = orders.DeliveryDate,
                    TotalQuantity = orders.TotalQuantity,
                    Status = (Data.Statuss)orders.Status
                };
                _context.Add(orderss);
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        //Cập nhật Order
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(Guid id, OrderModel model)
        {
            //Lấy Order từ ID được nhập
            var orderss = _context.Orders.SingleOrDefault(lo => lo.OrderID == id);
            //Kiểm tra orderss có được gán giá trị vào hay không?
            if (orderss != null)
            {
                orderss.OrderDate = model.OrderDate;
                orderss.CustomerID = model.CustomerID;
                orderss.DeliveryDate = model.DeliveryDate;
                orderss.TotalQuantity = model.TotalQuantity;
                orderss.Status = model.Status;
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        //Xóa Order
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(Guid id)
        {
            //Lấy Order từ ID được nhập
            var orderss = _context.Orders.SingleOrDefault(lo => lo.OrderID == id);
            //Kiểm tra orderss có được gán giá trị vào hay không?
            if (orderss != null)
            {
                _context.Orders.Remove(orderss);
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("api/[controller]")]
        public async Task<IActionResult> PagOrder(int page = 1, int pageSize = 10)
        {
            var totalItems = await _context.Orders.CountAsync();
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

            var order = await _context.Orders
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new
            {
                data = order,
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
