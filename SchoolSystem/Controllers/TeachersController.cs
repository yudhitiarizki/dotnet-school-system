using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using SchoolSystem.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeachersController> _logger;

        public TeachersController(ITeacherService teacherService, ILogger<TeachersController> logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            try
            {
                var teachers = await _teacherService.GetAllTeachersAsync();
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all teachers.");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // GET: api/Teachers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherByIdAsync(id);
                if (teacher == null)
                {
                    _logger.LogWarning("Teacher with ID {id} not found.", id);
                    return NotFound(new { message = $"Teacher with ID {id} not found." });
                }

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving teacher with ID {id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // POST: api/Teachers
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            try
            {
                await _teacherService.AddTeacherAsync(teacher);
                return CreatedAtAction(nameof(GetTeacher), new { id = teacher.Id }, new { message = "Teacher successfully created.", teacher });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new teacher.");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // PUT: api/Teachers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                _logger.LogWarning("Teacher ID mismatch for ID {id}.", id);
                return BadRequest(new { message = "Teacher ID mismatch." });
            }

            try
            {
                var existingTeacher = await _teacherService.GetTeacherByIdAsync(id);
                if (existingTeacher == null)
                {
                    _logger.LogWarning("Teacher with ID {id} not found.", id);
                    return NotFound(new { message = $"Teacher with ID {id} not found." });
                }

                await _teacherService.UpdateTeacherAsync(teacher);
                return Ok(new { message = "Teacher successfully updated.", teacher });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating teacher with ID {id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // DELETE: api/Teachers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherByIdAsync(id);
                if (teacher == null)
                {
                    _logger.LogWarning("Teacher with ID {id} not found.", id);
                    return NotFound(new { message = $"Teacher with ID {id} not found." });
                }

                await _teacherService.DeleteTeacherAsync(id);
                return Ok(new { message = "Teacher successfully deleted." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting teacher with ID {id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }
    }
}
