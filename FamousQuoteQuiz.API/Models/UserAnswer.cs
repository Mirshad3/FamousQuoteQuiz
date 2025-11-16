namespace FamousQuoteQuiz.API.Models
{
    public class UserAnswer
    {
        public int AnswerId { get; set; }
        public int SessionId { get; set; }
        public int QuoteId { get; set; }
        public string? UserAnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;

        public GameSession? Session { get; set; }
        public Quote? Quote { get; set; }

    }
}
