// Models/LoginViewModel.cs
using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required(ErrorMessage = "Username is required")]
    [Display(Name = "Username")]
    public string Username { get; set; } = string.Empty;  // Initialize with empty string

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;  // Initialize with empty string

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; } = false;         // Initialize with false

    [Required(ErrorMessage = "Role is required")]
    [Display(Name = "Role")]
    public string Role { get; set; } = "Admin";          // Already initialized
}