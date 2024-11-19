using Lab4;
using Lab5.Models;
using Lab5.Reader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

public class LabController: Controller
{

    public IActionResult Index()
    {
        if (!User.Identity?.IsAuthenticated ?? false)
        {
            return Redirect($"Account/Login/");
        }
        
        return View(new LabResultModel()
        {
            SelectedLab = "Lab1"
        });
    }

    [Authorize]
    [HttpPost]
    public IActionResult Index(LabResultModel model)
    {
        var lines = model.Input.Split("\n").ToList();
        var runner = new Runner(model.SelectedLab.ToLower(),
            new Lab5Reader(lines),
            new Lab5Saver(model)
        );
        
        runner.Run();
        return View(model);
    }
}