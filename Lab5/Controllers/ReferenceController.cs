using Lab5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

public class ReferenceController : Controller
{
    private readonly Lab6ApiService _referenceApiService;

    public ReferenceController(Lab6ApiService referenceApiService)
    {
        _referenceApiService = referenceApiService;
    }

    public async Task<IActionResult> Currencies()
    {
        var currencies = await _referenceApiService.GetCurrenciesAsync();
        return View(currencies);
    }

    public async Task<IActionResult> CardTypes()
    {
        var cardTypes = await _referenceApiService.GetCardTypesAsync();
        return View(cardTypes);
    }
    
    public async Task<IActionResult> CurrencyDetails(int id)
    {
        var currency = await _referenceApiService.GetCurrency(id);
        return View(currency);
    }
    
    public async Task<IActionResult> CardTypeDetails(int id)
    {
        var cardType = await _referenceApiService.GetCardType(id);
        return View(cardType);
    }
}