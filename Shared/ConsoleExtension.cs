namespace Shared;

public static class ConsoleExtension
{
    public static int GetInt(string message)
    {
        Console.WriteLine(message);
        var numberString = Console.ReadLine();
        if (int.TryParse(numberString, out int numberInt))
        {
            return numberInt;
        }

        return 0;
    }

    public static string? GetValidOptions(string message, List<string> options)
    {
        Console.Write(message);
        var answer = Console.ReadLine();
        if (options.Any(x => x.Equals(answer, StringComparison.CurrentCultureIgnoreCase)))
        {
            return answer;
        }
        return null;
    }
}
