using FamousQuoteQuiz.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamousQuoteQuiz.Api.Data;
public static class SeedData
{
    public static async Task EnsureSeedData(AppDbContext db)
    {
        if (!await db.Users.AnyAsync())
        {
            var pw = BCrypt.Net.BCrypt.HashPassword("password123");
            db.Users.Add(new User { FullName = "Alice Perera", Email = "alice@example.com", PasswordHash = pw });
            db.Users.Add(new User { FullName = "Ravi Kumar", Email = "ravi@example.com", PasswordHash = pw });
        }

        if (!await db.Quotes.AnyAsync())
        {
            db.Quotes.Add(new Quote {
                Text = "The only thing we have to fear is fear itself.",
                Author = "Franklin D. Roosevelt",
                OptionA = "Albert Einstein",
                OptionB = "Franklin D. Roosevelt",
                OptionC = "Winston Churchill"
            });
            db.Quotes.Add(new Quote {
                Text = "Life is what happens when you’re busy making other plans.",
                Author = "John Lennon",
                OptionA = "John Lennon",
                OptionB = "Mark Twain",
                OptionC = "Steve Jobs"
            });
        }
        await db.SaveChangesAsync();
    }
}
