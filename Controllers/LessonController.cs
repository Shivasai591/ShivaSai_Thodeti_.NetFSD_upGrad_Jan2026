using ElearningPlatform.DTOs;
using ElearningPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ElearningPlatform.Controllers;

[Authorize] // 🔐 Protect APIs
[ApiController]
[Route("api/lessons")]
public class LessonController : ControllerBase
{
    private readonly ILessonService _service;

    public LessonController(ILessonService service)
    {
        _service = service;
    }

    // ✅ GET /api/courses/{courseId}/lessons
    [HttpGet("/api/courses/{courseId}/lessons")]
    public async Task<IActionResult> GetLessons(int courseId)
    {
        var lessons = await _service.GetLessonsByCourse(courseId);

        if (lessons == null || !lessons.Any())
            return NotFound("No lessons found for this course");

        return Ok(lessons);
    }

    // ✅ POST /api/lesson
    [HttpPost]
    public async Task<IActionResult> CreateLesson([FromBody] CreateLessonDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var lesson = await _service.CreateLesson(dto);

        return CreatedAtAction(nameof(GetLessons), new { courseId = dto.CourseId }, lesson);
    }

    // ✅ PUT /api/lesson/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLesson(int id, [FromBody] CreateLessonDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateLesson(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    // ✅ DELETE /api/lesson/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLesson(int id)
    {
        var deleted = await _service.DeleteLesson(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}