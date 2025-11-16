namespace FamousQuoteQuiz.Api.Dtos;
public record QuoteDto(int Id, string Text, string Author, string OptionA, string OptionB, string OptionC, DateTime CreatedAt);
public record CreateQuoteDto(string Text, string Author, string OptionA, string OptionB, string OptionC);
public record UpdateQuoteDto(string? Text, string? Author, string? OptionA, string? OptionB, string? OptionC);
