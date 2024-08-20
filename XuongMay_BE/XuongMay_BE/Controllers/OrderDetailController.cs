using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;


namespace XuongMay_BE.Controllers
{
    [Authorize(Roles = "Supervisor")]
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
            try
            {
                var dsOrder = _context.OrderDetails.ToList();
                return Ok(dsOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving order details: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var orderDetail = _context.OrderDetails.SingleOrDefault(lo => lo.OrderID == id);

                if (orderDetail == null)
                {
                    return NotFound($"OrderDetail with ID {id} not found.");
                }

                return Ok(orderDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving the order detail: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateOrderDetail(OrderDetailModel orders)
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
                        
                        var orderDetail = new OrderDetail
                        {
                            OrderID = orders.OrderID,
                            ProductID = orders.ProductID,
                            SupervisorID = Guid.Parse(userID),
                            Price = orders.Price,
                            Quantity = orders.Quantity,
                            TotalPrice = orders.Price * orders.Quantity,
                        };
                        _context.Add(orderDetail);
                        _context.SaveChanges();
                        return Ok();
                    }
                }                
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating the order detail");
            }

        }


        //[HttpPut("{id}")]
        //public IActionResult UpdateOrderDetail(Guid id, OrderDetailModel orders)
        //{
        //    var orderss = _context.OrderDetails.SingleOrDefault(lo => lo.OrderID == id);
        //    if (orderss != null)
        //    {
        //        orderss.OrderID = orders.OrderID;
        //        orderss.ProductID = orders.ProductID;                
        //        orderss.Price = orders.Price;
        //        orderss.Quantity = orders.Quantity;
        //        orderss.TotalPrice = orders.Price * orders.Quantity;
        //        _context.SaveChanges();
        //        return NoContent();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteOrderDetail(Guid id)
        //{
        //    var orderss = _context.OrderDetails.SingleOrDefault(lo => lo.OrderDetailID == id);
        //    if (orderss != null)
        //    {
        //        _context.OrderDetails.Remove(orderss);
        //        _context.SaveChanges();
        //        return NoContent();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
    }
}
