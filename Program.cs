using Microsoft.Extensions.Configuration;
using SmartTask;
using SmartTask.Shared.Constants;
using SmartTask.Shared.FileProcessTask.Validators;
using SmartTask.Shared.Interfaces;
using SmartTask.Shared.Interfaces.FileProcessTask;
using SmartTask.Shared.Services;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var emailSettings = config.GetSection("EmailSettings").Get<EmailSettings>();

if (emailSettings == null)
{
    emailSettings = new EmailSettings();
}

IEmailService emailService = new SmtpEmailService(emailSettings);

var pathValidator = new PathValidator();
var existsValidator = new FileExistValidator();
var permissionValidator = new PermissionValidator();
var typeValidator = new FileTypeValidator();

pathValidator
    .SetNext(existsValidator)
    .SetNext(permissionValidator)
    .SetNext(typeValidator);

App app = new App(emailService, pathValidator);

app.Run();