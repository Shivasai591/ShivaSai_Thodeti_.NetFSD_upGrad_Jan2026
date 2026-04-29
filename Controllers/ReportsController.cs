using Microsoft.AspNetCore.Mvc;
using ElearningPlatform.Data;
using Microsoft.EntityFrameworkCore;

namespace ElearningPlatform.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReportsController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ AVG SCORE
    [HttpGet("average-score")]
    public async Task<IActionResult> GetAverageScore()
    {
        var avg = await _context.Results.AverageAsync(r => r.Score);
        return Ok(avg);
    }

    // ✅ GROUP BY (RESULT COUNT PER USER)
    [HttpGet("results-summary")]
    public async Task<IActionResult> GetSummary()
    {
        var data = await _context.Results
            .GroupBy(r => r.UserId)
            .Select(g => new
            {
                UserId = g.Key,
                TotalAttempts = g.Count(),
                AvgScore = g.Average(x => x.Score)
            })
            .ToListAsync();

        return Ok(data);
    }

    // ✅ SUBQUERY (ABOVE AVG USERS)
    [HttpGet("top-users")]
    public async Task<IActionResult> GetTopUsers()
    {
        var avg = await _context.Results.AverageAsync(r => r.Score);

        var users = await _context.Results
            .Where(r => r.Score > avg)
            .ToListAsync();

        return Ok(users);
    }

    // ✅ JOIN (USER + RESULTS)
    [HttpGet("user-results")]
    public async Task<IActionResult> GetUserResults()
    {
        var data = await _context.Results
            .Join(_context.Users,
                r => r.UserId,
                u => u.UserId,
                (r, u) => new
                {
                    u.FullName,
                    r.Score
                })
            .ToListAsync();

        return Ok(data);
    }
}