namespace SmartTask.Shared.Constants;

public enum TaskType
{
    Email = 1,
    File = 2,
    Calculation = 3
}

public class ValidationResult
{
    public bool IsValid { get; set; }
    public string Error { get; set; } = string.Empty;
}