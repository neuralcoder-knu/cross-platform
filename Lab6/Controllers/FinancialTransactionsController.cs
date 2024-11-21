using Lab6.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinancialTransactionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FinancialTransactionsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetTransactions()
    {
        var transactions = await _context.FinancialTransactions.ToListAsync();
        return Ok(transactions);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransaction(int id)
    {
        var transaction = await _context.FinancialTransactions
            .FirstOrDefaultAsync(t => t.TransactionId == id);

        if (transaction == null)
        {
            return NotFound();
        }
        
        var ukraineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Kiev");
        var transactionDateInUkraine = TimeZoneInfo.ConvertTimeToUtc(transaction.TransactionDate, ukraineTimeZone);

        transaction.TransactionDate = transactionDateInUkraine;

        return Ok(transaction);
    }
}