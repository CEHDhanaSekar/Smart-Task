namespace SmartTask.Tasks.Email;

public class EmailTaskConstant
{
    public enum TaskStatus
    {
        Pending,
        Validating,
        Failed,
        Completed
    }
}

public class EmailSettings
{
    public string FromEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
}
