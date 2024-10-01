using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using SchoolSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly ILogger<ClassesController> _logger;

        public ClassesController(IClassService classService, ILogger<ClassesController> logger)
        {
            _classService = classService;
            _logger = logger;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            try
            {
                var classes = await _classService.GetAllClassesAsync();
                return Ok(classes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all classes.");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // GET: api/Classes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            try
            {
                var classEntity = await _classService.GetClassByIdAsync(id);

                if (classEntity == null)
                {
                    _logger.LogWarning("Class with ID {id} not found.", id);
                    return NotFound(new { message = $"Class with ID {id} not found." });
                }

                return Ok(classEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving class with ID {id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // POST: api/Classes
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class classEntity)
        {
            try
            {
                await _classService.AddClassAsync(classEntity);
                return CreatedAtAction(nameof(GetClass), new { id = classEntity.Id }, new { message = "Class successfully created.", classEntity });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new class.");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // PUT: api/Classes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Class classEntity)
        {
            if (id != classEntity.Id)
            {
                _logger.LogWarning("Class ID mismatch for ID {id}.", id);
                return BadRequest(new { message = "Class ID mismatch." });
            }

            try
            {
                var existingClass = await _classService.GetClassByIdAsync(id);
                if (existingClass == null)
                {
                    _logger.LogWarning("Class with ID {id} not found.", id);
                    return NotFound(new { message = $"Class with ID {id} not found." });
                }

                await _classService.UpdateClassAsync(classEntity);
                return Ok(new { message = "Class successfully updated.", classEntity });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating class with ID {id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // DELETE: api/Classes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            try
            {
                var classEntity = await _classService.GetClassByIdAsync(id);
                if (classEntity == null)
                {
                    _logger.LogWarning("Class with ID {id} not found.", id);
                    return NotFound(new { message = $"Class with ID {id} not found." });
                }

                await _classService.DeleteClassAsync(id);
                return Ok(new { message = "Class successfully deleted." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting class with ID {id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }
    }
}
