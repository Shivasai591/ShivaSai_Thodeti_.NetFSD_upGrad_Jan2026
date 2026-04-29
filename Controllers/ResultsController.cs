using Microsoft.AspNetCore.Mvc;
using ElearningPlatform.Data;
using ElearningPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ElearningPlatform.Controllers
{
    [Authorize] // 🔐 Protect APIs
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ResultsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ POST: api/results
        [HttpPost]
        public async Task<IActionResult> AddResult([FromBody] Result result)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (result == null)
                return BadRequest("Invalid data");

            var userExists = await _context.Users.AnyAsync(u => u.UserId == result.UserId);
            var quizExists = await _context.Quizzes.AnyAsync(q => q.QuizId == result.QuizId);

            if (!userExists || !quizExists)
                return BadRequest("Invalid UserId or QuizId");

            try
            {
                await _context.Results.AddAsync(result);
                await _context.SaveChangesAsync();

                return Created("", new { message = "Result saved successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // ✅ GET: api/results/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetResults(int userId)
        {
            var results = await _context.Results
                .Where(r => r.UserId == userId)
                .AsNoTracking() // 🔥 Performance improvement
                .ToListAsync();

            if (results == null || results.Count == 0)
                return NotFound("No results found");

            return Ok(results);
        }
        // ✅ UNION EXAMPLE (SQL REQUIREMENT)
        [HttpGet("union-example")]
        public IActionResult UnionExample()
        {
            var data = _context.Users
                .Select(u => u.Email)
                .Union(_context.Users.Select(u => u.FullName))
                .ToList();

            return Ok(data);
        }
    }
}