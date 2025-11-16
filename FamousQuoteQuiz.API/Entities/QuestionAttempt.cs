namespace FamousQuoteQuiz.Api.Entities;
public class QuestionAttempt
{
    public int Id { get; set; }
    public int GameSessionId { get; set; }
    public GameSession? GameSession { get; set; }
    public int QuoteId { get; set; }
    public Quote? Quote { get; set; }
    public string SelectedAnswer { get; set; } = null!;
    public bool IsCorrect { get; set; }
    public DateTime ShownAt { get; set; } = DateTime.UtcNow;
}
