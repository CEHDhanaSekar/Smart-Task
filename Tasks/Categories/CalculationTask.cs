
using SmartTask.Shared.Constants;

namespace SmartTask.Tasks.Categories;

public class CalculationTask : BaseTask
{
    private readonly long _number;

    public CalculationTask(string name, long number) : base(name, "CALCULATION_TASK")
    {
        _number = number; 
    }

    public override ValidationResult Validate()
    {
        var res = new ValidationResult();
        if(_number < 1)
        {
            res.IsValid = false;
            res.Error = "Give a number more than 1";
        }
        res.IsValid = true;
        return res;
    }

    private long findFib(long num)
    {
        if (num == 0)
        {
            return num;
        }
        else if (num == 1) 
        {
            return num;
        }
        else
        {
            return (findFib(num - 2) + findFib(num - 1));
        }
    }

    private void PrintFibonacci()
    {
        for (int i = 0; i < _number; i++)
        {
            Console.WriteLine(findFib(i));
        }
    }

    public override async Task Execute()
    {
        Status.UpdateStatus("Validating Task...");
        var res = Validate();

        await Task.Delay(1000);
        if(!res.IsValid)
        {
            Status.UpdateStatus(res.Error);
            return;
        }
        Status.UpdateStatus("Task Validated");

        Status.UpdateStatus("Executing Task...");
        PrintFibonacci();
        Status.UpdateStatus("Fibonacci Series Printed");
        await Task.Delay(1000);
        PrintFactorial();
        Status.UpdateStatus("Factorial Calculated");
        await Task.Delay(1000);
        PrintPalindrome();
        Status.UpdateStatus("Palindrome Checked");
        await Task.Delay(1000);
        Status.UpdateStatus("Task Completed");
    }

    private long CalculateFactorial(long num)
    {
        if (num <= 1) return 1;
        long result = 1;
        for (long i = 2; i <= num; i++)
        {
            result *= i;
        }
        return result;
    }

    private void PrintFactorial()
    {
        Console.WriteLine($"Factorial of {_number}: {CalculateFactorial(_number)}");
    }

    private bool IsPalindrome(long num)
    {
        string s = num.ToString();
        for (int i = 0; i < s.Length / 2; i++)
        {
            if (s[i] != s[s.Length - 1 - i]) return false;
        }
        return true;
    }

    private void PrintPalindrome()
    {
        Console.WriteLine($"Is {_number} a palindrome? {IsPalindrome(_number)}");
    }
}
