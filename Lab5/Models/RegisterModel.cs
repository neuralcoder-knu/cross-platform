using System.ComponentModel.DataAnnotations;

namespace Lab5.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "Обов'язкове поле")]
    [MaxLength(50, ErrorMessage = "Ім'я користувача не повинно перевищувати 50 символів.")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Обов'язкове поле")]
    [MaxLength(500, ErrorMessage = "ФІО не повинно перевищувати 500 символів.")]
    public string FullName { get; set; }
    
    [Required(ErrorMessage = "Обов'язкове поле")]
    [StringLength(16, MinimumLength = 8, ErrorMessage = "Пароль повинен містити від 8 до 16 символів.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+={}\[\]|\\:;,.<>?/`~]).{8,16}$", 
        ErrorMessage = "Пароль повинен містити хоча б одну велику літеру, одну цифру та один спеціальний символ.")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Обов'язкове поле")]
    [Compare("Password", ErrorMessage = "Паролі не співпадають.")]
    public string ConfirmPassword { get; set; }
    
    [Required(ErrorMessage = "Телефон обов'язковий.")]
    [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Невірний формат телефону.")]
    public string Phone { get; set; }
    
    [Required(ErrorMessage = "Обов'язкове поле")]
    [EmailAddress(ErrorMessage = "Невірний формат електронної адреси.")]
    public string Email { get; set; }
}