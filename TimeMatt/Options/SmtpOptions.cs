namespace TimeMatt.Options;

public class SmtpOptions
{
    public const string SectionName = "Smtp";

    public string Host { get; set; } = "127.0.0.1";
    public int Port { get; set; } = 1025;
    public string FromName { get; set; } = "TextMatt";
    public string FromEmail { get; set; } = "no-reply@textmatt.dev";
}
