using System.ComponentModel.DataAnnotations;

namespace ElearningPlatform.DTOs
{
    public class SubmitQuizDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be valid")]
        public int UserId { get; set; }

        // QuestionId → Selected Answer
        [Required(ErrorMessage = "Answers are required")]
        [MinLength(1, ErrorMessage = "At least one answer must be provided")]
        public Dictionary<int, string> Answers { get; set; } = new();
    }
}