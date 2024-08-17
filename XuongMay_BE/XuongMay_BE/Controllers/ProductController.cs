using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;

namespace XuongMay_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyDbContext _context;
        public ProductController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var listProduct = _context.Products.ToList();
            return Ok(listProduct);
        }
        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            var product = _context.Products.FirstOrDefault(ca => ca.ProductID == id);
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult Create(ProductModel model)
        {
            try
            {
                var product = new Product()
                {
                    ProductName = model.ProductName,
                    Price = model.Price,
                    Description = model.Description,
                    CategoryID = model.CategoryID,
                };
                _context.Add(product);
                _context.SaveChanges();
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateByID(Guid id, ProductModel model)
        {
            var product = _context.Products.FirstOrDefault(ca => ca.ProductID == id);
            if (product != null)
            {
                product.ProductName = model.ProductName;
                product.Price = model.Price;
                product.Description = model.Description;
                product.CategoryID = model.CategoryID;
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
