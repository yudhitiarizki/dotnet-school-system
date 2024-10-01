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
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly ILogger<SubjectsController> _logger;

        public SubjectsController(ISubjectService subjectService, ILogger<SubjectsController> logger)
        {
            _subjectService = subjectService;
            _logger = logger;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            try
            {
                var subjects = await _subjectService.GetAllSubjectsAsync();
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all subjects.");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // GET: api/Subjects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSubject(int id)
        {
            try
            {
                var subject = await _subjectService.GetSubjectByIdAsync(id);

                if (subject == null)
                {
                    _logger.LogWarning("Subject with ID {id} not found.", id);
                    return NotFound(new { message = $"Subject with ID {id} not found." });
                }

                return Ok(subject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving subject with ID {id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // POST: api/Subjects
        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(Subject subject)
        {
            try
            {
                await _subjectService.AddSubjectAsync(subject);
                return CreatedAtAction(nameof(GetSubject), new { id = subject.Id }, new { message = "Subject successfully created.", subject });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new subject.");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // PUT: api/Subjects/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, Subject subject)
        {
            if (id != subject.Id)
            {
                _logger.LogWarning("Subject ID mismatch for ID {id}.", id);
                return BadRequest(new { message = "Subject ID mismatch." });
            }

            try
            {
                var existingSubject = await _subjectService.GetSubjectByIdAsync(id);
                if (existingSubject == null)
                {
                    _logger.LogWarning("Subject with ID {id} not found.", id);
                    return NotFound(new { message = $"Subject with ID {id} not found." });
                }

                await _subjectService.UpdateSubjectAsync(subject);
                return Ok(new { message = "Subject successfully updated.", subject });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating subject with ID {id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // DELETE: api/Subjects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            try
            {
                var subject = await _subjectService.GetSubjectByIdAsync(id);
                if (subject == null)
                {
                    _logger.LogWarning("Subject with ID {id} not found.", id);
                    return NotFound(new { message = $"Subject with ID {id} not found." });
                }

                await _subjectService.DeleteSubjectAsync(id);
                return Ok(new { message = "Subject successfully deleted." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting subject with ID {id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }
    }
}
