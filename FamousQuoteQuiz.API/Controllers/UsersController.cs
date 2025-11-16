using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamousQuoteQuiz.Api.Data;
using FamousQuoteQuiz.Api.Dtos;
using FamousQuoteQuiz.Api.Entities;

namespace FamousQuoteQuiz.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _db;
    public UsersController(AppDbContext db) => _db = db;

   

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound();
        return Ok(new UserDto(user.Id, user.FullName, user.Email, user.IsActive, user.CreatedAt));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto input)
    {
        if (await _db.Users.AnyAsync(u => u.Email == input.Email))
            return BadRequest("Email exists");

        var u = new User { FullName = input.FullName, Email = input.Email, PasswordHash = BCrypt.Net.BCrypt.HashPassword(input.Password) };
        _db.Users.Add(u);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = u.Id }, new UserDto(u.Id, u.FullName, u.Email, u.IsActive, u.CreatedAt));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateUserDto dto)
    {
        var u = await _db.Users.FindAsync(id);
        if (u == null) return NotFound();
        if (dto.FullName is not null) u.FullName = dto.FullName;
        if (dto.Email is not null) u.Email = dto.Email;
        if (dto.IsActive.HasValue) u.IsActive = dto.IsActive.Value;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var u = await _db.Users.FindAsync(id);
        if (u == null) return NotFound();
        _db.Users.Remove(u);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
