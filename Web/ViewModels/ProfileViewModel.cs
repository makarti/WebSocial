using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;

public class ProfileViewModel
{
    public Guid Id { get; set; }
    public string Login { get; set; }

    public string FirstName{get;set;}

    public string LastName{get;set;}

    public string City{get;set;}

    public string Gender{get;set;}

    public byte Age{get;set;}

    public string Interests{get;set;}
}