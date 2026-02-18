using Shared;

var answer = string.Empty;
var options = new List<string> { "s", "n" };

do
{
    Console.WriteLine("*** DATOS DE ENTRADA ***");

    var route = ConsoleExtension.GetInt("Ruta [1][2][3][4]...............................: ");
    var trips = ConsoleExtension.GetInt("Número de viajes................................: ");
    var passengers = ConsoleExtension.GetInt("Número de pasajeros total.......................: ");
    var packagesLess10 = ConsoleExtension.GetInt("Número de encomiendas de menos de 10Kg..........: ");
    var packages10To20 = ConsoleExtension.GetInt("Número de encomiendas entre 10Kg y menos de 20Kg: ");
    var packagesMore20 = ConsoleExtension.GetInt("Número de encomiendas de más de 20Kg............: ");

    Console.WriteLine("*** CALCULOS ***");

    var passengerIncome = CalculatePassengerIncome(route, trips, passengers);
    var packageIncome = CalculatePackageIncome(route, passengers, packagesLess10, packages10To20, packagesMore20);

    var totalIncome = passengerIncome + packageIncome;

    var assistantPayment = CalculateAssistantPayment(totalIncome);
    var insurancePayment = CalculateInsurancePayment(totalIncome);
    var fuelPayment = CalculateFuelPayment(route, passengers, packagesLess10, packages10To20, packagesMore20);

    var totalDeductions = assistantPayment + insurancePayment + fuelPayment;
    var totalToPay = totalIncome - totalDeductions;

    Console.WriteLine($"Ingresos por Pasajeros.........................: {passengerIncome,15:C2}");
    Console.WriteLine($"Ingresos por Encomiendas.......................: {packageIncome,15:C2}");
    Console.WriteLine("                                                :__________");
    Console.WriteLine($"TOTAL INGRESOS.................................: {totalIncome,15:C2}");
    Console.WriteLine($"Pago Ayudante..................................: {assistantPayment,15:C2}");
    Console.WriteLine($"Pago Seguro....................................: {insurancePayment,15:C2}");
    Console.WriteLine($"Pago Combustible...............................: {fuelPayment,15:C2}");
    Console.WriteLine("                                                :__________");
    Console.WriteLine($"TOTAL DEDUCCIONES..............................: {totalDeductions,15:C2}");
    Console.WriteLine("                                                :__________");
    Console.WriteLine($"TOTAL A LIQUIDAR...............................: {totalToPay,15:C2}");

    do
    {
        answer = ConsoleExtension.GetValidOptions("¿Deseas continuar [S]í, [N]o?: ", options);
    } while (!options.Any(x => x.Equals(answer, StringComparison.CurrentCultureIgnoreCase)));

} while (answer!.Equals("s", StringComparison.CurrentCultureIgnoreCase));

Console.WriteLine("Game over.");

decimal CalculatePassengerIncome(int route, int trips, int passengers)
{
    decimal routeValue = route switch
    {
        1 => 500000m,
        2 => 600000m,
        3 => 800000m,
        4 => 1000000m,
        _ => 0
    };

    decimal baseIncome = trips * routeValue;
    decimal commission = 0;

    if (passengers <= 50) commission = 0;
    else if (passengers <= 100)
        commission = route switch { 1 => 0.05m, 2 => 0.07m, 3 => 0.10m, 4 => 0.125m, _ => 0 };
    else if (passengers <= 150)
        commission = route switch { 1 => 0.06m, 2 => 0.08m, 3 => 0.13m, 4 => 0.15m, _ => 0 };
    else if (passengers <= 200)
        commission = route switch { 1 => 0.07m, 2 => 0.09m, 3 => 0.15m, 4 => 0.17m, _ => 0 };
    else
    {
        commission = route switch { 1 => 0.07m, 2 => 0.09m, 3 => 0.15m, 4 => 0.17m, _ => 0 };
        decimal extra = route switch { 1 => 50m, 2 => 60m, 3 => 100m, 4 => 150m, _ => 0 };
        return baseIncome + (baseIncome * commission) + ((passengers - 200) * extra);
    }

    return baseIncome + (baseIncome * commission);
}

decimal CalculatePackageIncome(int route, int passengers, int less10, int between10and20, int more20)
{
    int totalPackages = less10 + between10and20 + more20;

    int range = totalPackages switch
    {
        < 50 => 1,
        <= 100 => 2,
        <= 130 => 3,
        _ => 4
    };

    decimal valueLess10 = 0;
    decimal value10To20 = 0;
    decimal valueMore20 = 0;

    if (route <= 2)
    {
        valueLess10 = range switch { 1 => 100, 2 => 120, 3 => 150, _ => 160 };
        value10To20 = range switch { 1 => 120, 2 => 140, 3 => 160, _ => 180 };
        valueMore20 = value10To20;
    }
    else
    {
        valueLess10 = range switch { 1 => 130, 2 => 160, 3 => 175, _ => 200 };
        value10To20 = range switch { 1 => 140, 2 => 180, 3 => 200, _ => 250 };
        valueMore20 = range switch { 1 => 170, 2 => 210, 3 => 250, _ => 300 };
    }

    return (less10 * valueLess10) +
           (between10and20 * value10To20) +
           (more20 * valueMore20);
}

decimal CalculateAssistantPayment(decimal income)
{
    if (income < 1000000) return income * 0.05m;
    if (income <= 2000000) return income * 0.08m;
    if (income <= 4000000) return income * 0.10m;
    return income * 0.13m;
}

decimal CalculateInsurancePayment(decimal income)
{
    if (income < 1000000) return income * 0.03m;
    if (income <= 2000000) return income * 0.04m;
    if (income <= 4000000) return income * 0.06m;
    return income * 0.09m;
}

decimal CalculateFuelPayment(int route, int passengers, int less10, int between10and20, int more20)
{
    decimal km = route switch { 1 => 150, 2 => 167, 3 => 184, 4 => 203, _ => 0 };
    decimal gallons = km / 39m;
    decimal fuelCost = gallons * 8860m;

    decimal passengerWeight = passengers * 60m;
    decimal packageWeight = (less10 * 8m) + (between10and20 * 15m) + (more20 * 25m);
    decimal totalWeight = passengerWeight + packageWeight;

    decimal increase = 0;
    if (totalWeight > 10000) increase = 0.25m;
    else if (totalWeight > 5000) increase = 0.10m;

    fuelCost += fuelCost * increase;

    return fuelCost * 0.75m; // conductor paga 75% (empresa subsidia 25%)
}

