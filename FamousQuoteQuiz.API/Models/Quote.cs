namespace FamousQuoteQuiz.API.Models
{
    public class Quote
    {
        public int QuoteId { get; set; }
        public string QuoteText { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } 

        public ICollection<QuoteOption> Options { get; set; } = new List<QuoteOption>();

    }
}
