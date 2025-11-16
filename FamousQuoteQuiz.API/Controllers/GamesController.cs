using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamousQuoteQuiz.Api.Data;
using FamousQuoteQuiz.Api.Dtos;
using FamousQuoteQuiz.Api.Entities;

namespace FamousQuoteQuiz.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly AppDbContext _db;
    public GamesController(AppDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> Create(CreateGameSessionDto dto)
    {
        var user = await _db.Users.FindAsync(dto.UserId);
        if (user == null) return BadRequest("User not found.");

        var session = new GameSession { UserId = dto.UserId, StartedAt = DateTime.UtcNow };
        await _db.GameSessions.AddAsync(session);
        await _db.SaveChangesAsync();

        int score = 0;
        foreach (var a in dto.Attempts)
        {
            var quote = await _db.Quotes.FindAsync(a.QuoteId);
            if (quote == null) continue;

            bool isCorrect = string.Equals(quote.Author, a.SelectedAnswer, StringComparison.OrdinalIgnoreCase);
            if (isCorrect) score++;

            var attempt = new QuestionAttempt
            {
                GameSessionId = session.Id,
                QuoteId = a.QuoteId,
                SelectedAnswer = a.SelectedAnswer,
                IsCorrect = isCorrect,
                ShownAt = a.ShownAt
            };
            await _db.QuestionAttempts.AddAsync(attempt);
        }
        session.Score = score;
        session.FinishedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return Ok(new { sessionId = session.Id, score = session.Score });
    }
    
   
}
