using TimeMatt.Models;

namespace TimeMatt.Services;

public class ProjectService : IProjectService
{
    private readonly List<Project> _projects;
    private readonly List<ProjectFile> _files;
    private readonly List<ProjectComment> _comments;

    public ProjectService()
    {
        var today = DateTime.Today;
        _projects = new List<Project>
        {
            new() { Id = 1, Name = "Corporate Website", Description = "Full redesign of the corporate marketing site with a new CMS.", ClientId = 1, Status = ProjectStatus.Active, PaymentType = PaymentType.Hourly, Budget = 18000, HourlyRate = 85, Progress = 65, StartDate = today.AddDays(-40), Deadline = today.AddDays(12), HoursWorked = 138, EstimatedHours = 210, Color = "#7a4c8b" },
            new() { Id = 2, Name = "CRM Platform", Description = "Custom CRM for managing leads, deals and customer communication.", ClientId = 2, Status = ProjectStatus.Active, PaymentType = PaymentType.Hourly, Budget = 42000, HourlyRate = 95, Progress = 40, StartDate = today.AddDays(-60), Deadline = today.AddDays(45), HoursWorked = 176, EstimatedHours = 440, Color = "#22c55e" },
            new() { Id = 3, Name = "Portfolio Website", Description = "Minimal one-page portfolio site with case studies.", ClientId = 3, Status = ProjectStatus.Completed, PaymentType = PaymentType.Fixed, Budget = 6500, HourlyRate = 75, Progress = 100, StartDate = today.AddDays(-95), Deadline = today.AddDays(-20), HoursWorked = 86, EstimatedHours = 86, Color = "#0ea5e9" },
            new() { Id = 4, Name = "Mobile App", Description = "Cross-platform mobile app for in-store loyalty rewards.", ClientId = 4, Status = ProjectStatus.OnHold, PaymentType = PaymentType.Hourly, Budget = 35000, HourlyRate = 90, Progress = 28, StartDate = today.AddDays(-50), Deadline = today.AddDays(80), HoursWorked = 98, EstimatedHours = 390, Color = "#f59e0b" },
            new() { Id = 5, Name = "E-commerce Redesign", Description = "Storefront revamp with new checkout flow and product pages.", ClientId = 1, Status = ProjectStatus.Review, PaymentType = PaymentType.Fixed, Budget = 22000, HourlyRate = 85, Progress = 90, StartDate = today.AddDays(-70), Deadline = today.AddDays(5), HoursWorked = 232, EstimatedHours = 258, Color = "#14b8a6" },
            new() { Id = 6, Name = "Brand Identity", Description = "New logo, color system and brand guidelines package.", ClientId = 5, Status = ProjectStatus.Draft, PaymentType = PaymentType.Fixed, Budget = 9000, HourlyRate = 80, Progress = 5, StartDate = today.AddDays(-2), Deadline = today.AddDays(35), HoursWorked = 4, EstimatedHours = 112, Color = "#ec4899" },
            new() { Id = 7, Name = "Internal Tools", Description = "Internal dashboard for order tracking and reporting.", ClientId = 2, Status = ProjectStatus.Archived, PaymentType = PaymentType.Hourly, Budget = 15000, HourlyRate = 90, Progress = 100, StartDate = today.AddDays(-260), Deadline = today.AddDays(-180), HoursWorked = 166, EstimatedHours = 166, Color = "#64748b" },
        };

        _files = new List<ProjectFile>
        {
            new() { Id = 1, ProjectId = 1, FileName = "Homepage-Wireframes.fig", FileType = "Figma", FileSize = "4.2 MB", UploadedAt = today.AddDays(-18), UploadedBy = "You" },
            new() { Id = 2, ProjectId = 1, FileName = "Content-Copy-Deck.docx", FileType = "Word", FileSize = "128 KB", UploadedAt = today.AddDays(-10), UploadedBy = "Sarah Bennett" },
            new() { Id = 3, ProjectId = 1, FileName = "Brand-Assets.zip", FileType = "Archive", FileSize = "56.1 MB", UploadedAt = today.AddDays(-33), UploadedBy = "You" },
            new() { Id = 4, ProjectId = 2, FileName = "CRM-Data-Model.pdf", FileType = "PDF", FileSize = "890 KB", UploadedAt = today.AddDays(-25), UploadedBy = "You" },
            new() { Id = 5, ProjectId = 2, FileName = "API-Spec-v2.yaml", FileType = "YAML", FileSize = "44 KB", UploadedAt = today.AddDays(-6), UploadedBy = "Marcus Lee" },
            new() { Id = 6, ProjectId = 5, FileName = "Checkout-Flow.fig", FileType = "Figma", FileSize = "6.8 MB", UploadedAt = today.AddDays(-14), UploadedBy = "You" },
        };

        _comments = new List<ProjectComment>
        {
            new() { Id = 1, ProjectId = 1, Author = "Sarah Bennett", AuthorAvatar = "SB", Message = "The new hero section looks great! Can we make the CTA button a bit bigger?", PostedAt = today.AddDays(-3).AddHours(10) },
            new() { Id = 2, ProjectId = 1, Author = "You", AuthorAvatar = "YO", Message = "Sure, I'll bump it up and push a new preview link today.", PostedAt = today.AddDays(-3).AddHours(11) },
            new() { Id = 3, ProjectId = 1, Author = "Sarah Bennett", AuthorAvatar = "SB", Message = "Perfect, thank you!", PostedAt = today.AddDays(-2).AddHours(9) },
            new() { Id = 4, ProjectId = 2, Author = "Marcus Lee", AuthorAvatar = "ML", Message = "Can we prioritize the deal pipeline view for next sprint?", PostedAt = today.AddDays(-1).AddHours(15) },
            new() { Id = 5, ProjectId = 5, Author = "Sarah Bennett", AuthorAvatar = "SB", Message = "Checkout flow is looking solid, ready for final review.", PostedAt = today.AddHours(-6) },
        };
    }

    public List<Project> GetAll() => _projects;

    public Project? GetById(int id) => _projects.FirstOrDefault(p => p.Id == id);

    public List<Project> GetByClientId(int clientId) => _projects.Where(p => p.ClientId == clientId).ToList();

    public List<ProjectFile> GetFilesByProjectId(int projectId) => _files.Where(f => f.ProjectId == projectId).ToList();

    public List<ProjectComment> GetCommentsByProjectId(int projectId) => _comments.Where(c => c.ProjectId == projectId).OrderBy(c => c.PostedAt).ToList();
}
