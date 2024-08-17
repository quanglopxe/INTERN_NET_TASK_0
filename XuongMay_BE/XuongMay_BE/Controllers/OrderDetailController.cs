using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;

namespace XuongMay_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly MyDbContext _context;
        public OrderDetailController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var dsOrder = _context.OrderDetails.ToList();
            return Ok(dsOrder);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var orderss = _context.OrderDetails.SingleOrDefault(lo => lo.OrderID == id);
            if (orderss != null)
            {
                return Ok(orderss);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateOrderDetail(OrderDetailModel orders)
        {
            try
            {
                var orderDetail = new OrderDetail
                {
                    OrderID = orders.OrderID,
                    ProductID = orders.ProductID,
                    SupervisorID = orders.SupervisorID,
                    Price = orders.Price,
                    Quantity = orders.Quantity,
                    TotalPrice = orders.TotalPrice,
                };
                _context.Add(orderDetail);
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }


        [HttpPut("{id}")]
        public IActionResult UpdateOrderDetail(Guid id, OrderDetailModel orders)
        {
            var orderss = _context.OrderDetails.SingleOrDefault(lo => lo.OrderDetailID == id);
            if (orderss != null)
            {
                orderss.OrderID = orders.OrderID;
                orderss.ProductID = orders.ProductID;
                orderss.SupervisorID = orders.SupervisorID;
                orderss.Price = orders.Price;
                orderss.Quantity = orders.Quantity;
                orderss.TotalPrice = orders.TotalPrice;
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetail(Guid id)
        {
            var orderss = _context.OrderDetails.SingleOrDefault(lo => lo.OrderDetailID == id);
            if (orderss != null)
            {
                _context.OrderDetails.Remove(orderss);
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
