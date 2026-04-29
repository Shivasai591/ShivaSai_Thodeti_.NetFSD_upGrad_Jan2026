using ElearningPlatform.DTOs;

namespace ElearningPlatform.Services;

public interface IQuizService
{
    Task<IEnumerable<QuizDto>> GetQuizzesByCourse(int courseId);
    Task<QuizDto> CreateQuiz(CreateQuizDto dto);

    Task<IEnumerable<QuestionDto>> GetQuestions(int quizId);
    Task<QuestionDto> AddQuestion(CreateQuestionDto dto);

    Task<int> SubmitQuiz(int quizId, SubmitQuizDto dto);
}