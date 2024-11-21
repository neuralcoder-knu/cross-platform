using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SearchController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> SearchTransactions([FromBody] SearchRequest request)
    {
        var query = _context.FinancialTransactions
            .Include(ft => ft.Card)
            .ThenInclude(c => c.Cardholder)
            .Include(ft => ft.Merchant)
            .AsQueryable();

        if (request.StartDate.HasValue && request.EndDate.HasValue)
        {
            query = query.Where(ft => ft.TransactionDate >= request.StartDate && ft.TransactionDate <= request.EndDate);
        }

        if (request.CurrencyCodes != null && request.CurrencyCodes.Any())
        {
            query = query.Where(ft => request.CurrencyCodes.Contains(ft.CurrencyCode));
        }

        if (!string.IsNullOrEmpty(request.MerchantCodePrefix))
        {
            query = query.Where(ft => ft.Merchant.MerchantCategoryCode.StartsWith(request.MerchantCodePrefix));
        }

        var results = await query.ToListAsync();
        return Ok(results);
    }
}
