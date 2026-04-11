using SmartTask.Shared.Constants;
using SmartTask.Shared.Interfaces.FileProcessTask;

namespace SmartTask.Shared.FileProcessTask.Validators;

public abstract class BaseFileValidator : IValidatorChain, IFileValidator
{
    private IFileValidator? _next;

    public IValidatorChain SetNext(IFileValidator next)
    {
        _next = next;
        return this;
    }

    public virtual ValidationResult Validate(string filePath)
    {
        if (_next != null)
            return _next.Validate(filePath);

        return new ValidationResult { IsValid = true };
    }
}
