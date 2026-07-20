namespace TimeMatt.Models;

public class ProjectComment
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Author { get; set; } = string.Empty;
    public string AuthorAvatar { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime PostedAt { get; set; }
}
