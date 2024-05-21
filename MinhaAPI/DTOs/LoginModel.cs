using System.ComponentModel.DataAnnotations;

namespace MinhaAPI.DTOs;

public class LoginModel
{
    [Required(ErrorMessage ="User Name is required")]
    public string? Username { get; set; }
    [Required(ErrorMessage = "PassWord is required")]
    public string? Password { get; set; }
}
