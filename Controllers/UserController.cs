using AutoMapper;
using ElearningPlatform.Data;
using ElearningPlatform.DTOs;
using ElearningPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ElearningPlatform.Controllers;

[Authorize] // 🔐 Protect APIs
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // ✅ GET: api/user/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == id);

        if (user == null)
            return NotFound();

        var userDto = _mapper.Map<UserDto>(user);

        return Ok(userDto);
    }

    // ✅ PUT: api/user/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto dto)
    {
        // 🔥 VALIDATION
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound();

        // ✅ Update fields safely
        user.FullName = dto.FullName;
        user.Email = dto.Email;

        await _context.SaveChangesAsync();

        // 🔥 204 NoContent
        return NoContent();
    }
}