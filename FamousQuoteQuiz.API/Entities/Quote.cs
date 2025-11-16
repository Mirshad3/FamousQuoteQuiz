namespace FamousQuoteQuiz.Api.Entities;
public class Quote
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string OptionA { get; set; } = null!;
    public string OptionB { get; set; } = null!;
    public string OptionC { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
