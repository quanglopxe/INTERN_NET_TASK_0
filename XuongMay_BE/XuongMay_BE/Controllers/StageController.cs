using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;

namespace XuongMay_BE.Controllers
{
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
    }
}
