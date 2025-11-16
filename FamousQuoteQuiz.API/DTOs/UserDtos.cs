namespace FamousQuoteQuiz.Api.Dtos;
public record UserDto(int Id, string FullName, string Email, bool IsActive, DateTime CreatedAt);
public record CreateUserDto(string FullName, string Email, string Password);
public record UpdateUserDto(string FullName, string? Email, bool? IsActive);
