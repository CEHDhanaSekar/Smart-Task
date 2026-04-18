namespace SmartTask.Core.Helpers;

public class CommonHelper
{
    public static string ReadMultilineInput(string showText)
    {
        Console.WriteLine($"{showText} (type 'END' to finish):");

        var lines = new List<string>();

        while (true)
        {
            var line = Console.ReadLine();

            if (line?.Trim().ToUpper() == "END")
                break;

            lines.Add(line ?? "");
        }

        return string.Join(Environment.NewLine, lines);
    }
}
