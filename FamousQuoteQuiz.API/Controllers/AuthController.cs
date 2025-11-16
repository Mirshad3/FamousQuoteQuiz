using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamousQuoteQuiz.Api.Data;
using FamousQuoteQuiz.Api.Dtos;
using FamousQuoteQuiz.Api.Entities;
using FamousQuoteQuiz.Api.Services;

namespace FamousQuoteQuiz.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly TokenService _tokenService;
    public AuthController(AppDbContext db, TokenService tokenService) { _db = db; _tokenService = tokenService; }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (await _db.Users.AnyAsync(u => u.Email == dto.Email)) return BadRequest("Email exists");

        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = new User { FullName = dto.FullName, Email = dto.Email, PasswordHash = hash };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var token = _tokenService.CreateToken(user.Id, user.Email, user.FullName);
        return Ok(new { token, user = new { user.Id, user.FullName, user.Email } });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null) return Unauthorized("Invalid credentials");
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) return Unauthorized("Invalid credentials");

        var token = _tokenService.CreateToken(user.Id, user.Email, user.FullName);
        return Ok(new { token, user = new { user.Id, user.FullName, user.Email } });
    }
}
