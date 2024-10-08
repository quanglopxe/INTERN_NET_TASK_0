﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XuongMay_BE.Data;
using XuongMay_BE.Models;

namespace XuongMay_BE.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class SupervisorController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SupervisorController(MyDbContext context)
        {
            _context = context;
        }

        // API GET để tìm toàn bộ Supervisor 
        [HttpGet]
        public IActionResult GetAll()
        {
            var listSupervisor = _context.Supervisors.ToList();
            return Ok(listSupervisor);
        }

        // API POST để tạo Supervisor mới
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(SupervisorModel model)
        {
            try
            {
                var supervisor = new Supervisor()
                {
                    SupervisorName = model.SupervisorName,   
                    UserID = model.UserID
                };
                _context.Supervisors.Add(supervisor);
                _context.SaveChanges();
                return Ok(supervisor);
            }
            catch
            {
                return BadRequest();
            }
        }

        // API GET để tìm Supervisor theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var supervisor = await _context.Supervisors
                .FirstOrDefaultAsync(s => s.SupervisorID == id);

            if (supervisor == null)
            {
                return NotFound();
            }

            return Ok(supervisor);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupervisor(Guid id)
        {
            // Tìm kiếm supervisor theo ID
            var supervisor = await _context.Supervisors.FindAsync(id);

            if (supervisor == null)
            {
                return NotFound();
            }

            // Xóa supervisor
            _context.Supervisors.Remove(supervisor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupervisor(Guid id, SupervisorModel model)
        {
            try
            {
                // Tìm kiếm Supervisor theo ID
                var supervisor = await _context.Supervisors.FirstOrDefaultAsync(s => s.SupervisorID == id);
                if (supervisor == null)
                {
                    return NotFound($"Supervisor with ID {id} not found.");
                }

                // Cập nhật thông tin Supervisor
                supervisor.SupervisorName = model.SupervisorName;
                supervisor.UserID = model.UserID;

                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A concurrency error occurred while updating the supervisor.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the supervisor.");
            }


        }

        [HttpGet("api/[controller]")]
        public async Task<IActionResult> PagSupervisor(int page = 1, int pageSize = 10)
        {
            var totalItems = await _context.Supervisors.CountAsync();
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

            var sup = await _context.Supervisors
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new
            {
                data = sup,
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
        // Kiểm tra xem Supervisor có tồn tại không
        private bool SupervisorExists(Guid id)
        {
            return _context.Supervisors.Any(e => e.SupervisorID == id);
        }

    }
}
