using System.ComponentModel.DataAnnotations;

namespace ElearningPlatform.DTOs
{
    public class CreateLessonDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "CourseId must be valid")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required")]
        [StringLength(2000, ErrorMessage = "Content cannot exceed 2000 characters")]
        public string Content { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "OrderIndex must be greater than 0")]
        public int OrderIndex { get; set; }
    }
}