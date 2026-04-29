using ElearningPlatform.DTOs;

namespace ElearningPlatform.Services;

public interface ILessonService
{
    Task<IEnumerable<LessonDto>> GetLessonsByCourse(int courseId);
    Task<LessonDto> CreateLesson(CreateLessonDto dto);
    Task<bool> UpdateLesson(int id, CreateLessonDto dto);
    Task<bool> DeleteLesson(int id);
}