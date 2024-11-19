using System.Security.Claims;
using System.Text.Json;
using Lab5.Models;
using Lab5.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

public class AccountController(AuthorizationService authorizationService) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var isRegistered = await authorizationService.RegisterUserAsync(
            email: model.Email,
            password: model.Password,
            fullName: model.FullName,
            phoneNumber: model.Phone,
            username: model.Username
        );

        if (isRegistered) return await Login(model.Email, model.Password);
        
        ModelState.AddModelError(string.Empty, "Помилка під час реєстрації.");
        return View(model);
    }
    
    public IActionResult Register()
    {
        return View(new RegisterModel());
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var accessToken = await authorizationService.LoginUserAsync(email, password);
        if (accessToken == null)
        {
            ModelState.AddModelError(string.Empty, "Невірний логін або пароль.");
            return View();
        }
        
        var userInfo = await authorizationService. GetUserInfoAsync(accessToken);
        var nickname = userInfo?.GetString("nickname");
        var givenName = userInfo?.GetString("given_name");
        
        var phone = (userInfo?.TryGetProperty("user_metadata", out _) ?? false)
            ? userInfo?.GetProperty("user_metadata").GetString("phone")
            : "";
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, email),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, nickname ?? ""),
            new Claim("FullName", givenName ?? ""),
            new Claim(ClaimTypes.OtherPhone, phone ?? "")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties();

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), authProperties);

        return RedirectToAction("Profile");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
    
    [Authorize]
    public IActionResult Profile()
    {
        var claims = new List<Claim>(User.Claims);
       
        return View(new ProfileModel()
        {
            Username = claims[2].Value,
            Email = claims[1].Value,
            FullName = claims[3].Value,
            Phone = claims[4].Value
        });
    }
}