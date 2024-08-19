using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;
using Microsoft.AspNetCore.Authorization;


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
            try
            {
                var lstStage = _context.Stage.ToList();
                return Ok(lstStage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving stages: {ex.Message}");
            }
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
                return NotFound($"Stage with ID {id} not found.");
            }
        }

        [HttpPost]
        public IActionResult createStage(StageModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating the stage");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateByID(Guid id, StageModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stage = _context.Stage.FirstOrDefault(st => st.StageID == id);
            if (stage != null)
            {
                stage.StageName = model.StageName;
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while updating the stage");
            }
        }
    }
}
