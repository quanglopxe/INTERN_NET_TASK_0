using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;

namespace XuongMay_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MyDbContext _context;
        public OrdersController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var dsOrder = _context.Orders.ToList();
            return Ok(dsOrder);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Orders orderss = _context.Orders.SingleOrDefault(s => s.OrderID == id);
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
        public IActionResult CreateOrder(OrderModel orders)
        {
            try
            {
                var orderss = new Orders
                {
                    OrderDate = orders.OrderDate,
                    CustomerID = orders.CustomerID,
                    DeliveryDate = orders.DeliveryDate,
                    TotalQuantity = orders.TotalQuantity,
                    Status = orders.Status
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


        [HttpPut("{id}")]
        public IActionResult UpdateOrder(Guid id, OrderModel model)
        {
            var orderss = _context.Orders.SingleOrDefault(lo => lo.OrderID == id);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(Guid id)
        {
            var orderss = _context.Orders.SingleOrDefault(lo => lo.OrderID == id);
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

    }
}
