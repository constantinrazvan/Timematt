using System.Globalization;
using TimeMatt.Models;

namespace TimeMatt.ViewModels;

public static class BadgeHelpers
{
    private static readonly CultureInfo MoneyCulture = CultureInfo.GetCultureInfo("en-US");

    public static string Money(this decimal value) => value.ToString("C0", MoneyCulture);

    public static string Hours(this double value) => value.ToString("0.#", MoneyCulture);

    public static string CssClass(this ProjectStatus status) => status switch
    {
        ProjectStatus.Draft => "fh-badge-draft",
        ProjectStatus.Active => "fh-badge-active",
        ProjectStatus.OnHold => "fh-badge-onhold",
        ProjectStatus.Review => "fh-badge-review",
        ProjectStatus.Completed => "fh-badge-completed",
        ProjectStatus.Archived => "fh-badge-archived",
        _ => "fh-badge-draft"
    };

    public static string Label(this ProjectStatus status) => status switch
    {
        ProjectStatus.OnHold => "On Hold",
        _ => status.ToString()
    };

    public static string CssClass(this TaskPriority priority) => priority switch
    {
        TaskPriority.Low => "fh-badge-low",
        TaskPriority.Medium => "fh-badge-medium",
        TaskPriority.High => "fh-badge-high",
        TaskPriority.Urgent => "fh-badge-urgent",
        _ => "fh-badge-low"
    };

    public static string ColumnLabel(this TaskColumn column) => column switch
    {
        TaskColumn.InProgress => "In Progress",
        _ => column.ToString()
    };

    public static string CssClass(this PaymentType type) => type == PaymentType.Fixed ? "fh-badge-fixed" : "fh-badge-hourly";

    public static string Label(this PaymentType type) => type == PaymentType.Fixed ? "Fixed Price" : "Hourly";

    public static string CssClass(this AccountRole role) => role switch
    {
        AccountRole.Owner => "fh-badge-fixed",
        AccountRole.TeamMember => "fh-badge-active",
        AccountRole.Client => "fh-badge-review",
        _ => "fh-badge-draft"
    };

    public static string Label(this AccountRole role) => role switch
    {
        AccountRole.TeamMember => "Team Member",
        _ => role.ToString()
    };
}
