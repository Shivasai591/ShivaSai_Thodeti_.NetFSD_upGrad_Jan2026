using ElearningPlatform.Data;
using ElearningPlatform.DTOs;
using ElearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ElearningPlatform.Services;

public class QuizService : IQuizService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public QuizService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<QuizDto>> GetQuizzesByCourse(int courseId)
    {
        var quizzes = await _context.Quizzes
            .Where(q => q.CourseId == courseId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<QuizDto>>(quizzes);
    }

    public async Task<QuizDto> CreateQuiz(CreateQuizDto dto)
    {
        var quiz = _mapper.Map<Quiz>(dto);

        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();

        return _mapper.Map<QuizDto>(quiz);
    }

    public async Task<IEnumerable<QuestionDto>> GetQuestions(int quizId)
    {
        var questions = await _context.Questions
            .Where(q => q.QuizId == quizId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<QuestionDto>>(questions);
    }

    public async Task<QuestionDto> AddQuestion(CreateQuestionDto dto)
    {
        var question = _mapper.Map<Question>(dto);

        _context.Questions.Add(question);
        await _context.SaveChangesAsync();

        return _mapper.Map<QuestionDto>(question);
    }
    public async Task<int> SubmitQuiz(int quizId, SubmitQuizDto dto)
    {
        var questions = await _context.Questions
            .Where(q => q.QuizId == quizId)
            .ToListAsync();

        int score = 0;

        foreach (var q in questions)
        {
            if (dto.Answers.ContainsKey(q.QuestionId))
            {
                var userAnswer = dto.Answers[q.QuestionId];

                if (userAnswer == q.CorrectAnswer)
                    score++;
            }
        }

        // 🔥 SAVE RESULT
        var result = new Result
        {
            UserId = dto.UserId,
            QuizId = quizId,
            Score = score
        };

        _context.Results.Add(result);
        await _context.SaveChangesAsync();

        return score;
    }
}