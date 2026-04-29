namespace ElearningPlatform.Models;

public class Lesson
{
    public int LessonId { get; set; }

    public int CourseId { get; set; }   // FK
    public Course Course { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public int OrderIndex { get; set; }
}