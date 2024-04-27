using System.ComponentModel.DataAnnotations;

namespace MeetingOrganizer.Web.Pages.Auth;

public class RegisterModel
{
    [Required(ErrorMessage = "Name is required.")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email is invalid.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MaxLength(50, ErrorMessage = "Password can not be more than 50 characters long.")]
    public string Password { get; set; }
}
