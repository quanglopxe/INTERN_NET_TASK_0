using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace XuongMay_BE.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StageController : ControllerBase
    {
        private readonly MyDbContext _context;
        public StageController(MyDbContext context) { _context = context; }

        [HttpGet]
        public IActionResult getAll()
        {
            var lstStage = _context.Stage.ToList();
            return Ok(lstStage);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            var stage = _context.Employees.FirstOrDefault(st => st.EmpID == id);
            if (stage != null)
            {
                return Ok(stage);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult createStage(StageModel model)
        {
            try
            {
                var stage = new Stage()
                {
                    StageName = model.StageName,
                    Description = model.Description,
                    Sequence = model.Sequence
                };
                _context.Add(stage);
                _context.SaveChanges();
                return Ok(stage);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateByID(Guid id, StageModel model)
        {
            var stage = _context.Stage.FirstOrDefault(st => st.StageID == id);
            if (stage != null)
            {
                stage.StageName = model.StageName;
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("api/[controller]")]
        public async Task<IActionResult> PagStage(int page = 1, int pageSize = 10)
        {
            var totalItems = await _context.Stage.CountAsync();
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

            var stage = await _context.Stage
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new
            {
                data = stage,
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
