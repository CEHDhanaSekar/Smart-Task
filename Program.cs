using Microsoft.Extensions.Configuration;
using SmartTask.Shared.Constants;
using SmartTask.Shared.Interfaces;
using SmartTask.Shared.Services;
using SmartTask;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var emailSettings = config.GetSection("EmailSettings").Get<EmailSettings>();

if (emailSettings == null)
{
    emailSettings = new EmailSettings();
}

IEmailService emailService = new SmtpEmailService(emailSettings);

App app = new App(emailService);

app.Run();