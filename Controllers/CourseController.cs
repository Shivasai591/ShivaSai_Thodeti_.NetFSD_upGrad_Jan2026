using Microsoft.AspNetCore.Mvc;
using ElearningPlatform.DTOs;
using ElearningPlatform.Services;
using Microsoft.AspNetCore.Authorization;

namespace ElearningPlatform.Controllers;

[Authorize] // 🔐 Protect all APIs
[ApiController]
[Route("api/courses")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;
    private readonly ILogger<CourseController> _logger; // 🔥 LOGGER

    public CourseController(ICourseService courseService, ILogger<CourseController> logger)
    {
        _courseService = courseService;
        _logger = logger;
    }

    // ✅ GET: api/course
    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        _logger.LogInformation("Fetching all courses");

        var courses = await _courseService.GetAllCourses();
        return Ok(courses);
    }

    // ✅ GET: api/course/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(int id)
    {
        _logger.LogInformation("Fetching course with ID: {Id}", id);

        var course = await _courseService.GetCourseById(id);

        if (course == null)
        {
            _logger.LogWarning("Course not found with ID: {Id}", id);
            return NotFound();
        }

        return Ok(course);
    }

    // ✅ POST: api/course
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto dto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid course data received");
            return BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Creating new course: {Title}", dto.Title);

            var course = await _courseService.CreateCourse(dto);

            _logger.LogInformation("Course created successfully with ID: {Id}", course.CourseId);

            return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, course);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating course");
            return StatusCode(500, ex.Message);
        }
    }

    // ✅ PUT: api/course/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] CreateCourseDto dto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid update data for course ID: {Id}", id);
            return BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Updating course with ID: {Id}", id);

            var updated = await _courseService.UpdateCourse(id, dto);

            if (!updated)
            {
                _logger.LogWarning("Course not found for update, ID: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("Course updated successfully, ID: {Id}", id);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating course ID: {Id}", id);
            return StatusCode(500, ex.Message);
        }
    }

    // ✅ DELETE: api/course/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        _logger.LogInformation("Deleting course with ID: {Id}", id);

        var deleted = await _courseService.DeleteCourse(id);

        if (!deleted)
        {
            _logger.LogWarning("Course not found for deletion, ID: {Id}", id);
            return NotFound();
        }

        _logger.LogInformation("Course deleted successfully, ID: {Id}", id);

        return NoContent();
    }
}