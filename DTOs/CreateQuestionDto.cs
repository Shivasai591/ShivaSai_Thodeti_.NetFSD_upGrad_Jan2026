using System.ComponentModel.DataAnnotations;

namespace ElearningPlatform.DTOs
{
    public class CreateQuestionDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "QuizId must be valid")]
        public int QuizId { get; set; }

        [Required(ErrorMessage = "Question text is required")]
        [StringLength(500, ErrorMessage = "Question cannot exceed 500 characters")]
        public string QuestionText { get; set; } = string.Empty;

        [Required(ErrorMessage = "Option A is required")]
        [StringLength(200)]
        public string OptionA { get; set; } = string.Empty;

        [Required(ErrorMessage = "Option B is required")]
        [StringLength(200)]
        public string OptionB { get; set; } = string.Empty;

        [Required(ErrorMessage = "Option C is required")]
        [StringLength(200)]
        public string OptionC { get; set; } = string.Empty;

        [Required(ErrorMessage = "Option D is required")]
        [StringLength(200)]
        public string OptionD { get; set; } = string.Empty;

        [Required(ErrorMessage = "Correct answer is required")]
        [RegularExpression("OptionA|OptionB|OptionC|OptionD|Language|OS|Browser|Editor",
            ErrorMessage = "CorrectAnswer must match one of the options")]
        public string CorrectAnswer { get; set; } = string.Empty;
    }
}