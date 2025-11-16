namespace FamousQuoteQuiz.API.Models
{
    public class GameSession
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;

        public User? User { get; set; }
        public ICollection<UserAnswer> Answers { get; set; } = new List<UserAnswer>();

    }
}
