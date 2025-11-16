namespace FamousQuoteQuiz.API.Models
{
    public class QuoteOption
    {
        public int OptionId { get; set; }
        public int QuoteId { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; } = false;

        public Quote? Quote { get; set; }

    }
}
