using SmartTask.Shared.Interfaces;
using System.Net.Mail;
using System.Net;
using SmartTask.Shared.Constants;

namespace SmartTask.Shared.Services;

public sealed class SmtpEmailService : IEmailService
{
    private readonly string _fromEmail;
    private readonly string _password;
    private readonly string _host;
    private readonly int _port;

    public SmtpEmailService(EmailSettings emailSettings)
    {
        _fromEmail = emailSettings.FromEmail;
        _password = emailSettings.Password;
        _host = emailSettings.Host;
        _port = emailSettings.Port;
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        var fromAddress = new MailAddress(_fromEmail, "System");
        var toAddress = new MailAddress(to);

        var smtp = new SmtpClient
        {
            Host = _host,
            Port = _port,
            EnableSsl = true,
            Credentials = new NetworkCredential(_fromEmail, _password)
        };

        using var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        };

        await smtp.SendMailAsync(message);
    }
}