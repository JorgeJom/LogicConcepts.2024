using Shared;

var answer = string.Empty;
var options = new List<string> { "s", "n" };

do
{
    var n = ConsoleExtension.GetInt("¿Cáuntos números desea?: ");
    var suma = 0;

    for (int i = 1; i <= n; i++)
    {
        Console.Write($"{i,4} ");

        if (i % 15 == 0)
        {
            Console.WriteLine();
        }
        suma += i;
    }
    Console.WriteLine();

    var promedio = suma / n;

    Console.WriteLine($"La suma es: {suma,10:N0}");
    Console.WriteLine($"El promedio es: {promedio,6:N0}");

    do
    {
        answer = ConsoleExtension.GetValidOptions("¿Deseas continuar [S]í, [N]o?: ", options);
    } while (!options.Any(x => x.Equals(answer, StringComparison.CurrentCultureIgnoreCase)));
} while (answer!.Equals("s", StringComparison.CurrentCultureIgnoreCase));

Console.WriteLine("Game over.");
