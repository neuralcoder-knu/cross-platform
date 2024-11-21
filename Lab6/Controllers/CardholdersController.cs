using Lab6.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardholdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CardholdersController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCardholders()
    {
        var cardholders = await _context.Cardholders.ToListAsync();
        return Ok(cardholders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCardholder(int id)
    {
        var cardholder = await _context.Cardholders.FirstOrDefaultAsync(c => c.CardholderId == id);
        if (cardholder == null) return NotFound();
        return Ok(cardholder);
    }
}