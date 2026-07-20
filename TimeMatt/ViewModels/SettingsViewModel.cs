using System.ComponentModel.DataAnnotations;

namespace TimeMatt.ViewModels;

public class SettingsViewModel
{
    public string Name { get; set; } = "Alex Morgan";
    public string Email { get; set; } = "alex.morgan@TextMatt.dev";
    public string Company { get; set; } = "Morgan Digital Studio";
    public string Currency { get; set; } = "USD";
    public string Timezone { get; set; } = "(UTC-05:00) Eastern Time";
    public string Theme { get; set; } = "System";
}

public class ChangePasswordViewModel
{
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Current Password")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "New Password")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    public string NewPassword { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm New Password")]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }
    public bool Success { get; set; }
}
