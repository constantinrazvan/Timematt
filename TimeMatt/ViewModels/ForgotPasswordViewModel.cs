using System.ComponentModel.DataAnnotations;

namespace TimeMatt.ViewModels;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public bool EmailSent { get; set; }
}
