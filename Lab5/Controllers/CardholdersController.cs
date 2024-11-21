using Lab5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

public class CardholdersController : Controller
{
    private readonly Lab6ApiService _service;

    public CardholdersController(Lab6ApiService service)
    {
        _service = service;
    }
    
    public async Task<IActionResult> Index()
    {
        var cardholders = await _service.GetCardholdersAsync();
        return View(cardholders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var cardholder = await _service.GetCarholder(id);
        return View(cardholder);
    }
}