﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


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
        //API lấy tất cả danh sách Product
        [HttpGet]
        public IActionResult GetAll()
        {
            var listProduct = _context.Products.ToList();
            return Ok(listProduct);
        }
        //API lấy Product từ ID
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
        //Kiểm tra quyền Admin mới được thêm Product
        [Authorize(Roles = "Admin")]
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
        //Kiểm tra quyền Admin mới được sửa Product
        [Authorize(Roles = "Admin")]
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
        //Kiểm tra quyền Admin mới được xóa Product
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Tìm kiếm product theo ID
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // Xóa product
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //API phân trang Product
        [HttpGet("api/[controller]")]
        public async Task<IActionResult> PagProduct(int page = 1, int pageSize = 10)
        {
            var totalItems = await _context.Products.CountAsync();
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

            var pro = await _context.Products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new
            {
                data = pro,
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
