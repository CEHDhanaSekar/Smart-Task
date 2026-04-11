namespace SmartTask.Shared.Interfaces.FileProcessTask;

public interface IValidatorChain
{
    IValidatorChain SetNext(IFileValidator next);
}
