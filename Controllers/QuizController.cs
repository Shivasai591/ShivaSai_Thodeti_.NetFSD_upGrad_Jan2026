using ElearningPlatform.DTOs;
using ElearningPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ElearningPlatform.Controllers;

[Authorize] // 🔐 Protect APIs
[ApiController]
[Route("api/quizzes")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _service;

    public QuizController(IQuizService service)
    {
        _service = service;
    }

    // ✅ GET /api/quiz/{courseId}
    [HttpGet("{courseId}")]
    public async Task<IActionResult> GetQuizzes(int courseId)
    {
        var quizzes = await _service.GetQuizzesByCourse(courseId);

        if (quizzes == null || !quizzes.Any())
            return NotFound("No quizzes found for this course");

        return Ok(quizzes);
    }

    // ✅ POST /api/quiz
    [HttpPost]
    public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var quiz = await _service.CreateQuiz(dto);
            return CreatedAtAction(nameof(GetQuizzes), new { courseId = dto.CourseId }, quiz);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // ✅ GET /api/quiz/{quizId}/questions
    [HttpGet("{quizId}/questions")]
    public async Task<IActionResult> GetQuestions(int quizId)
    {
        var questions = await _service.GetQuestions(quizId);

        if (questions == null || !questions.Any())
            return NotFound("No questions found for this quiz");

        return Ok(questions);
    }

    // ✅ POST /api/questions
    [HttpPost("/api/questions")]
    public async Task<IActionResult> AddQuestion([FromBody] CreateQuestionDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var question = await _service.AddQuestion(dto);
            return CreatedAtAction(nameof(GetQuestions), new { quizId = dto.QuizId }, question);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // ✅ POST /api/quiz/{quizId}/submit
    [HttpPost("{quizId}/submit")]
    public async Task<IActionResult> SubmitQuiz(int quizId, [FromBody] SubmitQuizDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var score = await _service.SubmitQuiz(quizId, dto);
            return Ok(new { score });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}