using Microsoft.EntityFrameworkCore;
using FamousQuoteQuiz.Api.Entities;

namespace FamousQuoteQuiz.Api.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
    public DbSet<User> Users => Set<User>();
    public DbSet<Quote> Quotes => Set<Quote>();
    public DbSet<GameSession> GameSessions => Set<GameSession>();
    public DbSet<QuestionAttempt> QuestionAttempts => Set<QuestionAttempt>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<GameSession>().HasOne(gs => gs.User).WithMany(u => u.GameSessions).HasForeignKey(gs => gs.UserId);
        modelBuilder.Entity<QuestionAttempt>().HasOne(a => a.GameSession).WithMany(gs => gs.Attempts).HasForeignKey(a => a.GameSessionId);
        modelBuilder.Entity<QuestionAttempt>().HasOne(a => a.Quote).WithMany().HasForeignKey(a => a.QuoteId);
    }
}
