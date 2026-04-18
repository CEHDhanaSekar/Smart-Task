using SmartTask.Core.Constants;

namespace SmartTask.Tasks.FileProcessing.Validators;

public class PathValidator : BaseFileValidator
{
    public override ValidationResult Validate(string filePath)
    {
        var res = new ValidationResult { IsValid = true };

        // 1. Null / Empty
        if (string.IsNullOrWhiteSpace(filePath))
        {
            res.IsValid = false;
            res.Error = "File path is null or empty";
            return res;
        }

        // 2. Invalid Path Characters
        if (filePath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
        {
            res.IsValid = false;
            res.Error = "Path contains invalid characters";
            return res;
        }

        try
        {
            var fullPath = Path.GetFullPath(filePath);

            // 3. File Name Validation
            string fileName = Path.GetFileName(fullPath);
            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                res.IsValid = false;
                res.Error = "File name contains invalid characters";
                return res;
            }

            // 4. Root Validation
            if (!Path.IsPathRooted(fullPath))
            {
                res.IsValid = false;
                res.Error = "Path is not rooted (absolute path required)";
                return res;
            }

            // 5. Length Check
            if (fullPath.Length > 260)
            {
                res.IsValid = false;
                res.Error = "Path exceeds maximum allowed length";
                return res;
            }

            // ? If all checks passed
            res.IsValid = true;
            return res;
        }
        catch (Exception ex)
        {
            res.IsValid = false;
            res.Error = $"Invalid path format: {ex.Message}";
            return res;
        }
    }
}
