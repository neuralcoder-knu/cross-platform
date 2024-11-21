using Lab5.Models;
using Lab5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

public class SearchController : Controller
{
    private readonly Lab6ApiService _service;

    public SearchController(Lab6ApiService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Results(SearchViewModel searchViewModel)
    {
        return View(await _service.SearchTransaction(searchViewModel));
    }
}