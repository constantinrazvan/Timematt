namespace TimeMatt.Models;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public AccountRole Role { get; set; }
    public string Company { get; set; } = string.Empty;
    public DateTime LastActive { get; set; }
}
