namespace FamousQuoteQuiz.Api.Dtos;
public record QuestionAttemptDto(int QuoteId, string SelectedAnswer, bool IsCorrect, DateTime ShownAt);
public record CreateGameSessionDto(int UserId, List<QuestionAttemptDto> Attempts);
