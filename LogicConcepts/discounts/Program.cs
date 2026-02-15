using Shared;

var answer = string.Empty;
var options = new List<string> { "s", "n" };

do
{
    var desk = ConsoleExtension.GetInt("Número de escritorios: ");
    var valueToPay = CalculateValue(desk);

    Console.WriteLine($"El valor a pagar es..: {valueToPay:C2}");

    do
    {
        answer = ConsoleExtension.GetValidOptions("¿Deseas continuar [S]í, [N]o?: ", options);
    } while (!options.Any(x => x.Equals(answer, StringComparison.CurrentCultureIgnoreCase)));

} while (answer!.Equals("s", StringComparison.CurrentCultureIgnoreCase));

static decimal CalculateValue(int desk)
{
    float discount;
    if (desk < 5)
    {
        discount = 0.1f;
    }
    else if (desk < 10)
    {
        discount = 0.2f;
    }
    else
    {
        discount = 0.4f;
    }

    return desk * 650000M * (decimal)(1 - discount);
}

Console.WriteLine("Game over.");
