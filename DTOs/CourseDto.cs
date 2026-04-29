namespace ElearningPlatform.DTOs;

public class CourseDto
{
    public int CourseId { get; set; }     // ✅ IMPORTANT
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}