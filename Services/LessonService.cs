using ElearningPlatform.Data;
using ElearningPlatform.DTOs;
using ElearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ElearningPlatform.Services;

public class LessonService : ILessonService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public LessonService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LessonDto>> GetLessonsByCourse(int courseId)
    {
        var lessons = await _context.Lessons
            .Where(l => l.CourseId == courseId)
            .OrderBy(l => l.OrderIndex)
            .ToListAsync();

        return _mapper.Map<IEnumerable<LessonDto>>(lessons);
    }

    public async Task<LessonDto> CreateLesson(CreateLessonDto dto)
    {
        var lesson = _mapper.Map<Lesson>(dto);

        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        return _mapper.Map<LessonDto>(lesson);
    }

    public async Task<bool> UpdateLesson(int id, CreateLessonDto dto)
    {
        var lesson = await _context.Lessons.FindAsync(id);

        if (lesson == null)
            return false;

        lesson.Title = dto.Title;
        lesson.Content = dto.Content;
        lesson.OrderIndex = dto.OrderIndex;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteLesson(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);

        if (lesson == null)
            return false;

        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync();

        return true;
    }
}