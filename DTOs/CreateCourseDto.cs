using System.ComponentModel.DataAnnotations;

namespace ElearningPlatform.DTOs
{
    public class CreateCourseDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "CreatedBy must be a valid UserId")]
        public int CreatedBy { get; set; }
    }
}