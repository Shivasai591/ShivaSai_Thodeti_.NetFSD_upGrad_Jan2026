using ElearningPlatform.Data;
using ElearningPlatform.DTOs;
using ElearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ElearningPlatform.Services;

public class CourseService : ICourseService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CourseService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // ✅ FIX 4 — AsNoTracking (Performance)
    public async Task<IEnumerable<CourseDto>> GetAllCourses()
    {
        var courses = await _context.Courses
            .AsNoTracking() // 🔥 Performance improvement
            .ToListAsync();

        return _mapper.Map<IEnumerable<CourseDto>>(courses);
    }

    public async Task<CourseDto?> GetCourseById(int id)
    {
        var course = await _context.Courses
            .AsNoTracking() // 🔥 Added here also
            .FirstOrDefaultAsync(c => c.CourseId == id);

        if (course == null)
            return null;

        return _mapper.Map<CourseDto>(course);
    }

    // ✅ FIX 5 — Eager Loading (Include Lessons)
    public async Task<CourseDto?> GetCourseWithLessons(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Lessons) // 🔥 Eager loading
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CourseId == id);

        if (course == null)
            return null;

        return _mapper.Map<CourseDto>(course);
    }

    public async Task<CourseDto> CreateCourse(CreateCourseDto dto)
    {
        var course = _mapper.Map<Course>(dto);

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return _mapper.Map<CourseDto>(course);
    }

    public async Task<bool> UpdateCourse(int id, CreateCourseDto dto)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course == null)
            return false;

        course.Title = dto.Title;
        course.Description = dto.Description;
        course.CreatedBy = dto.CreatedBy;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course == null)
            return false;

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return true;
    }
}