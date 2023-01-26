using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Login is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "ConfirmPassword is required")]
    [Compare(nameof(Password), ErrorMessage = "Passwords are not equal")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "FirstName is required")]
    [StringLength(45)]
    public string FirstName{get;set;}

    [Required(ErrorMessage = "LastName is required")]
    [StringLength(45)]
    public string LastName{get;set;}

    [Required(ErrorMessage = "City is required")]
    [StringLength(100)]
    public string City{get;set;}

    [Required(ErrorMessage = "Gender is required")]
    public byte Gender{get;set;}

    [Required(ErrorMessage = "Age is required")]
    public byte Age{get;set;}

    [Required(ErrorMessage = "Interest is required")]
    [StringLength(255)]
    public string Interests{get;set;}
}