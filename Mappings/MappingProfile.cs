using AutoMapper;
using ElearningPlatform.Models;
using ElearningPlatform.DTOs;

namespace ElearningPlatform
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ✅ ADD THIS LINE
            CreateMap<CreateCourseDto, Course>();
            CreateMap<Course, CourseDto>();

            CreateMap<Lesson, LessonDto>();
            CreateMap<CreateLessonDto, Lesson>();

            CreateMap<Quiz, QuizDto>();
            CreateMap<CreateQuizDto, Quiz>();

            CreateMap<Question, QuestionDto>();
            CreateMap<CreateQuestionDto, Question>();
            CreateMap<User, UserDto>();
        }
    }
}