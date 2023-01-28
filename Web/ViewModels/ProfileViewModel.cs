using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;

public class ProfileViewModel
{
    public string Login { get; set; }

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