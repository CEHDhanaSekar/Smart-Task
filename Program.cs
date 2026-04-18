using Microsoft.Extensions.Configuration;
using SmartTask;
using SmartTask.Core.Constants;
using SmartTask.Tasks.FileProcessing.Validators;
using SmartTask.Core.Interfaces;
using SmartTask.Tasks.FileProcessing.Interfaces;
using SmartTask.Tasks.Email;

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

await app.RunAsync();
