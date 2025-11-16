namespace FamousQuoteQuiz.API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } 

        public ICollection<GameSession> Sessions { get; set; } = new List<GameSession>();

    }
}
