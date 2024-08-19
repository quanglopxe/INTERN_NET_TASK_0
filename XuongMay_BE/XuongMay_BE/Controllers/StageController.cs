﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongMay_BE.Data;
using XuongMay_BE.Models;
using Microsoft.AspNetCore.Authorization;

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
        //[Authorize(Roles = AppRole.Admin)]
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
        //[Authorize(Roles = AppRole.Admin)]
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
    }
}
