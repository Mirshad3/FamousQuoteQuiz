using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamousQuoteQuiz.Api.Data;
using FamousQuoteQuiz.Api.Dtos;
using FamousQuoteQuiz.Api.Entities;

namespace FamousQuoteQuiz.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class QuotesController : ControllerBase
{
    private readonly AppDbContext _db;
    public QuotesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var q = _db.Quotes.OrderByDescending(x => x.CreatedAt)
              .Skip((page - 1) * pageSize).Take(pageSize);
        var list = await q.ToListAsync();
        var dto = list.Select(x => new QuoteDto(x.Id, x.Text, x.Author, x.OptionA, x.OptionB, x.OptionC, x.CreatedAt));
        return Ok(dto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var q = await _db.Quotes.FindAsync(id);
        if (q == null) return NotFound();
        return Ok(new QuoteDto(q.Id, q.Text, q.Author, q.OptionA, q.OptionB, q.OptionC, q.CreatedAt));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateQuoteDto dto)
    {
        var q = new Quote { Text = dto.Text, Author = dto.Author, OptionA = dto.OptionA, OptionB = dto.OptionB, OptionC = dto.OptionC };
        _db.Quotes.Add(q);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = q.Id }, new QuoteDto(q.Id, q.Text, q.Author, q.OptionA, q.OptionB, q.OptionC, q.CreatedAt));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateQuoteDto dto)
    {
        var q = await _db.Quotes.FindAsync(id);
        if (q == null) return NotFound();
        if (dto.Text is not null) q.Text = dto.Text;
        if (dto.Author is not null) q.Author = dto.Author;
        if (dto.OptionA is not null) q.OptionA = dto.OptionA;
        if (dto.OptionB is not null) q.OptionB = dto.OptionB;
        if (dto.OptionC is not null) q.OptionC = dto.OptionC;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var q = await _db.Quotes.FindAsync(id);
        if (q == null) return NotFound();
        _db.Quotes.Remove(q);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
