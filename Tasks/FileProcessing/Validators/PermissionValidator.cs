using SmartTask.Core.Constants;
using System.IO;

namespace SmartTask.Tasks.FileProcessing.Validators;

public class PermissionValidator : BaseFileValidator
{
    public override ValidationResult Validate(string filePath)
    {
        var res = new ValidationResult { IsValid = true };

        try
        {
            using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                res.IsValid = true;
            }
        }
        catch (UnauthorizedAccessException)
        {
            res.IsValid = false;
            res.Error = "No permission to read the file";
        }
        catch (FileNotFoundException)
        {
            res.IsValid = false;
            res.Error = "File not found";
        }
        catch (IOException)
        {
            res.IsValid = false;
            res.Error = "File is locked or in use";
        }

        return res;
    }
}
