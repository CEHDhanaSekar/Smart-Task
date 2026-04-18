namespace SmartTask.Tasks.FileProcessing.Interfaces;

public interface IValidatorChain
{
    IValidatorChain SetNext(IFileValidator next);
}
