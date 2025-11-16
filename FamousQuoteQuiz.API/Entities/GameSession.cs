namespace FamousQuoteQuiz.Api.Entities;
public class GameSession
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? FinishedAt { get; set; }
    public int Score { get; set; }
    public ICollection<QuestionAttempt> Attempts { get; set; } = new List<QuestionAttempt>();
}
