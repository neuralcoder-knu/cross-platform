using Lab6.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReferenceController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ReferenceController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("Currencies")]
    public async Task<IActionResult> GetCurrencies()
    {
        var currencies = await _context.RefCurrencyCodes.ToListAsync();
        return Ok(currencies);
    }
    
    
    [HttpGet("CardTypes")]
    public async Task<IActionResult> GetCardTypes()
    {
        var cardTypes = await _context.RefCardTypes.ToListAsync();
        return Ok(cardTypes);
    }
    
    [HttpGet("CardTypes/{id}")]
    public async Task<IActionResult> GetCardType(int id)
    {
        var currency = await _context.RefCardTypes.FirstOrDefaultAsync(c => c.CardTypeCode == id);
        if (currency == null) return NotFound();
        return Ok(currency);
    }
    
    [HttpGet("Currencies/{id}")]
    public async Task<IActionResult> GetCurrency(int id)
    {
        var currency = await _context.RefCurrencyCodes.FirstOrDefaultAsync(c => c.CurrencyCode == id);
        if (currency == null) return NotFound();
        return Ok(currency);
    }
}