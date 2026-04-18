using SmartTask.Core.Constants;
using SmartTask.Core.Tasks;
using System.Net.Mail;

namespace SmartTask.Tasks.Email;

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

    public void SetEmail(string email)
    {
        Email = email;
    }

    public void SetSubject(string subject)
    {
        Subject = subject;
    }

    public void SetBody(string body)
    {
        Body = body;
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

        await Task.Delay(1000);
        if (!res.IsValid)
        {
            Status.UpdateStatus($"Validation failed. {res.Error}");
            var fallbackRes = FallBack(ref res);
            if (!fallbackRes)
            {
                Status.UpdateStatus("Task Cancelled...");
                return;
            }
        }

        Status.UpdateStatus("Validation Completed...");

        try
        {
            Status.UpdateStatus("Sending Mail...");

            await SendEmailAsync();

            Status.UpdateStatus("Mail Successfully sent...");
            await Task.Delay(1000);
            Status.UpdateStatus("Task Completed...");
        }
        catch (Exception ex)
        {
            Status.UpdateStatus($"Failed - {ex.Message}");
        }
    }

    private bool FallBack(ref ValidationResult res)
    {
        while (!res.IsValid)
        {
            Console.WriteLine("Email is invalid");
            Console.WriteLine("1. Want to Re-Enter Email");
            Console.WriteLine("2. Want to Exit");
            Console.Write("Enter choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                choice = 0;
            }

            if (choice == 1)
            {
                Console.Write("Enter Email: ");
                string email = Console.ReadLine() ?? "";
                SetEmail(email);

                res = Validate();
                if (res.IsValid)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Validation failed. {res.Error}");
                }
            }
            else
            {
                res.IsValid = false;
                return false;
            }
        }
        return true;
    }
}
