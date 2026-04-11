using SmartTask.Shared.Interfaces;
using SmartTask.Shared.Constants;
using System.Net.Mail;

namespace SmartTask.Tasks;

public class EmailTask : BaseTask
{
    private string Email;
    private string Subject;
    private string Body;
    private IEmailService _emailService;

    public EmailTask(string name, string email, string subject, string body, IEmailService emailService) : base(name, "EMAIL_TASK")
    {
        Email = email;
        Subject = subject;
        Body = body;
        _emailService = emailService;
    }

    public async Task SendEmailAsync()
    {
        await _emailService.SendAsync(Email, Subject, Body);
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public override ValidationResult Validate()
    {
        if (Email == null) return new ValidationResult { IsValid = false, Error = "Email is null" };

        if (IsValidEmail(Email))
        {
            return new ValidationResult { IsValid = true };
        };

        return new ValidationResult { IsValid = false, Error = "Invalid email format" };
    }

    public override async Task Execute()
    {
        Status.UpdateStatus("Validating...");

        var res = Validate();

        if (!res.IsValid)
        {
            Status.UpdateStatus($"Validation failed. {res.Error}");
            return;
        }

        Status.UpdateStatus("Validation Completed...");

        try
        {
            Status.UpdateStatus("Sending Mail...");

            await SendEmailAsync();

            Status.UpdateStatus("Mail Successfully sent...");
            Status.UpdateStatus("Task Completed...");
        }
        catch (Exception ex)
        {
            Status.UpdateStatus($"Failed - {ex.Message}");
        }
    }
}
