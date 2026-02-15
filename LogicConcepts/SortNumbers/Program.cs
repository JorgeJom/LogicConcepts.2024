using Shared;

var answer = string.Empty;
var options = new List<string> { "s", "n" };

do
{
    Console.WriteLine("Ingrese 3 números diferentes");
    var a = ConsoleExtension.GetInt("Ingrese primer número: ");
    var b = ConsoleExtension.GetInt("Ingrese segundo número: ");
    if (a == b)
    {
        Console.WriteLine("Los números deben ser diferentes, comience de nuevo");
        continue;
    }
    var c = ConsoleExtension.GetInt("Ingrese tercer número: ");
    if (b == c || a == c)
    {
        Console.WriteLine("Los números deben ser diferentes, comience de nuevo");
        continue;
    }

    if (a > b && a > c)
    {
        if (b > c)
        {
            Console.WriteLine($"El número mayor es {a}, el medio es {b} y el menor es {c}");
        }
        else
        {
            Console.WriteLine($"El número mayor es {a}, el medio es {c} y el menor es {b}");
        }
    }
    else if (b > a && b > c)
    {
        if (a > c)
        {
            Console.WriteLine($"El número mayor es {b}, el medio es {a} y el menor es {c}");
        }
        else
        {
            Console.WriteLine($"El número mayor es {b}, el medio es {c} y el menor es {a}");
        }
    }
    else
    {
        if (a > b)
        {
            Console.WriteLine($"El número mayor es {c}, el medio es {a} y el menor es {b}");
        }
        else
        {
            Console.WriteLine($"El número mayor es {c}, el medio es {b} y el menor es {a}");
        }
    }

    do
    {
        answer = ConsoleExtension.GetValidOptions("¿Deseas continuar [S]í, [N]o?: ", options);
    } while (!options.Any(x => x.Equals(answer, StringComparison.CurrentCultureIgnoreCase)));

} while (answer!.Equals("s", StringComparison.CurrentCultureIgnoreCase));

Console.WriteLine("Game over.");