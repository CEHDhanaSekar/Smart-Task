using SmartTask.Core.Constants;
using SmartTask.Core.Tasks;

namespace SmartTask.Tasks.Calculation;

public class CalculationTask : BaseTask
{
    private long _number;

    public CalculationTask(string name, long number) : base(name, "CALCULATION_TASK")
    {
        _number = number;
    }

    public void SetNumber(long number)
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
        else
        {
            res.IsValid = true;
        }
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
            Status.UpdateStatus($"Validation failed. {res.Error}");
            var fallbackRes = FallBack(ref res);
            if (!fallbackRes)
            {
                Status.UpdateStatus("Task Cancelled...");
                return;
            }
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

    private bool FallBack(ref ValidationResult res)
    {
        while (!res.IsValid)
        {
            Console.WriteLine("Number is invalid");
            Console.WriteLine("1. Want to Re-Enter Number");
            Console.WriteLine("2. Want to Exit");
            Console.Write("Enter choice: ");
            
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                choice = 0;
            }

            if (choice == 1)
            {
                Console.Write("Enter Number: ");
                if (!long.TryParse(Console.ReadLine(), out long number))
                {
                    number = 0;
                }
                SetNumber(number);
                
                res = Validate();
                if (res.IsValid)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Validation failed. {res.Error}");
                }
            }
            else
            {
                res.IsValid = false;
                return false;
            }
        }
        return true;
    }
}
