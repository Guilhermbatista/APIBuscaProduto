using System.ComponentModel.DataAnnotations;

namespace MinhaAPI.DTOs;

public class RegisterModel
{
    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public String? Password { get; set; }
}
