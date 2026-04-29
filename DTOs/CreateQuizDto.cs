using System.ComponentModel.DataAnnotations;

namespace ElearningPlatform.DTOs
{
    public class CreateQuizDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "CourseId must be valid")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = string.Empty;
    }
}