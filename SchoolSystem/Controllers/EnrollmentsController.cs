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
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly ILogger<EnrollmentsController> _logger;

        public EnrollmentsController(IEnrollmentService enrollmentService, ILogger<EnrollmentsController> logger)
        {
            _enrollmentService = enrollmentService;
            _logger = logger;
        }

        // GET: api/Enrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollments()
        {
            try
            {
                var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving enrollments.");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // GET: api/Enrollments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
        {
            try
            {
                var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);

                if (enrollment == null)
                {
                    _logger.LogWarning("Enrollment with ID {id} not found.", id);
                    return NotFound(new { message = $"Enrollment with ID {id} not found." });
                }

                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving enrollment with ID {id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // POST: api/Enrollments
        [HttpPost]
        public async Task<ActionResult<Enrollment>> PostEnrollment(Enrollment enrollment)
        {
            try
            {
                await _enrollmentService.AddEnrollmentAsync(enrollment);
                return CreatedAtAction(nameof(GetEnrollment), new { id = enrollment.Id }, enrollment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new enrollment.");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // PUT: api/Enrollments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrollment(int id, Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                _logger.LogWarning("Enrollment ID mismatch for ID {id}.", id);
                return BadRequest(new { message = "Enrollment ID mismatch." });
            }

            try
            {
                var existingEnrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
                if (existingEnrollment == null)
                {
                    _logger.LogWarning("Enrollment with ID {id} not found.", id);
                    return NotFound(new { message = $"Enrollment with ID {id} not found." });
                }

                await _enrollmentService.UpdateEnrollmentAsync(enrollment);
                return Ok(new { message = "Enrollment successfully updated.", enrollment });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating enrollment with ID {id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // DELETE: api/Enrollments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            try
            {
                var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
                if (enrollment == null)
                {
                    _logger.LogWarning("Enrollment with ID {id} not found.", id);
                    return NotFound(new { message = $"Enrollment with ID {id} not found." });
                }

                await _enrollmentService.DeleteEnrollmentAsync(id);
                return Ok(new { message = "Enrollment successfully deleted." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting enrollment with ID {id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }
    }
}
