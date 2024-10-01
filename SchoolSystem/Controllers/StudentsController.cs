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
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all students.");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // GET: api/Students/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                if (student == null)
                {
                    _logger.LogWarning("Student with ID {id} not found.", id);
                    return NotFound(new { message = $"Student with ID {id} not found." });
                }

                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving student with ID {id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            try
            {
                await _studentService.AddStudentAsync(student);
                return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, new { message = "Student successfully created.", student });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new student.");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // PUT: api/Students/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            try
            {
                var existingStudent = await _studentService.GetStudentByIdAsync(id);
                if (existingStudent == null)
                {
                    _logger.LogWarning("Student with ID {id} not found.", id);
                    return NotFound(new { message = $"Student with ID {id} not found." });
                }

                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                existingStudent.DateOfBirth = student.DateOfBirth;

                await _studentService.UpdateStudentAsync(existingStudent);
                return Ok(new { message = "Student successfully updated.", student = existingStudent });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating student with ID {id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // DELETE: api/Students/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                if (student == null)
                {
                    _logger.LogWarning("Student with ID {id} not found.", id);
                    return NotFound(new { message = $"Student with ID {id} not found." });
                }

                await _studentService.DeleteStudentAsync(id);
                return Ok(new { message = "Student successfully deleted." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting student with ID {id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }
    }
}
