namespace ElearningPlatform.Models;

public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int CreatedBy { get; set; }
    public User? User { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<Lesson>? Lessons { get; set; }
    public ICollection<Quiz>? Quizzes { get; set; }
}