using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.Application.Models.Identity;

public class RegistrationRequest
{
    [Required]
    public string FirstName {get; set;}

    [Required]
    public string LastName {get; set;}

    [Required]
    public string Email {get; set;}

    [Required]
    public string UserName {get; set;}

    [Required]
    public string Password{get; set;}

    [Required]
    public ICollection<string> Roles {get; set;}
}
