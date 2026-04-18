namespace SmartTask.Tasks.FileProcessing;

public class FileTaskHelpers
{
    public enum FileType
    {
        Unknown = 0,
        Txt,
        Json,
        Xml,
    }

    public static string[] AllowedFileTypes = { ".txt", ".json", ".xml" };

    public static FileType GetFileType(string filePath)
    {
        var ext = GetFileExtension(filePath);

        return ext switch
        {
            ".txt" => FileType.Txt,
            ".json" => FileType.Json,
            ".xml" => FileType.Xml,
            _ => FileType.Unknown
        };
    }

    private static string GetFileExtension(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return string.Empty;

        return Path.GetExtension(filePath).ToLowerInvariant();
    }
}
