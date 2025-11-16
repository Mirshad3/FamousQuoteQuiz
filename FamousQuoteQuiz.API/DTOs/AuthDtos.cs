namespace FamousQuoteQuiz.Api.Dtos;
public record RegisterDto(string FullName, string Email, string Password);
public record LoginDto(string Email, string Password);
