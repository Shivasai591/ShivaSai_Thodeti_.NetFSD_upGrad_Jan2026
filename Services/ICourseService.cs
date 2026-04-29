using ElearningPlatform.DTOs;

namespace ElearningPlatform.Services;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetAllCourses();
    Task<CourseDto?> GetCourseById(int id);
    Task<CourseDto> CreateCourse(CreateCourseDto dto);
    Task<bool> UpdateCourse(int id, CreateCourseDto dto);
    Task<bool> DeleteCourse(int id);
}