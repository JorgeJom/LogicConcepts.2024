using Shared;

var answer = string.Empty;
var options = new List<string> { "s", "n" };

do
{
    Console.WriteLine("*** DATOS DE ENTRADA ***");

    var cc = ConsoleExtension.GetDecimal("Costo de compra ($)....................................................: ");

    var tpOptions = new List<string> { "p", "n" };
    string tp;
    do
    {
        tp = ConsoleExtension.GetValidOptions("Tipo de producto [P]erecedero, [N]o perecedero.........................: ", tpOptions)!;
    } while (!tpOptions.Any(x => x.Equals(tp, StringComparison.CurrentCultureIgnoreCase)));

    var tcOptions = new List<string> { "f", "a" };
    string tc;
    do
    {
        tc = ConsoleExtension.GetValidOptions("Tipo de conservación [F]rio, [A]mbiente................................: ", tcOptions)!;
    } while (!tcOptions.Any(x => x.Equals(tc, StringComparison.CurrentCultureIgnoreCase)));

    var pc = ConsoleExtension.GetInt("Periodo de conservación (días).........................................: ");
    var pa = ConsoleExtension.GetInt("Periodo de almacenamiento (días).......................................: ");
    var vol = ConsoleExtension.GetDecimal("Volumen (litros).......................................................: ");

    var maOptions = new List<string> { "n", "c", "e", "g" };
    string ma;
    do
    {
        ma = ConsoleExtension.GetValidOptions("Medio de almacenamiento [N]evera, [C]ongelador, [E]estanteria, [G]uacal: ", maOptions)!;
    } while (!maOptions.Any(x => x.Equals(ma, StringComparison.CurrentCultureIgnoreCase)));

    Console.WriteLine("*** CALCULOS ***");

    var ca = CalculateStorageCost(cc, tp, tc, pc, pa, vol);
    var pdp = CalculateDepreciation(pa);
    var ce = CalculateExhibitionCost(tp, tc, ma, ca);

    var vrp = (cc + ca + ce) * pdp;
    var vrv = tp.Equals("n", StringComparison.CurrentCultureIgnoreCase) ? vrp * 1.20m : vrp * 1.40m;

    Console.WriteLine($"Costos de almacenamiento..............................................: {ca,15:C2}");
    Console.WriteLine($"Porcentaje de depreciación............................................:   {pdp,15:P2}");
    Console.WriteLine($"Costo de exhibición...................................................: {ce,15:C2}");
    Console.WriteLine($"Valor producto........................................................: {vrp,15:C2}");
    Console.WriteLine($"Valor venta...........................................................: {vrv,15:C2}");

    do
    {
        answer = ConsoleExtension.GetValidOptions("¿Deseas continuar [S]í, [N]o?: ", options);
    } while (!options.Any(x => x.Equals(answer, StringComparison.CurrentCultureIgnoreCase)));

} while (answer!.Equals("s", StringComparison.CurrentCultureIgnoreCase));

Console.WriteLine("Game over.");


//  FUNCIONES 

decimal CalculateStorageCost(decimal cc, string tp, string tc, int pc, int pa, decimal vol)
{
    // PERECEDEROS
    if (tp.Equals("p", StringComparison.CurrentCultureIgnoreCase))
    {
        if (tc.Equals("f", StringComparison.CurrentCultureIgnoreCase))
        {
            return pc < 10 ? cc * 0.05m : cc * 0.10m;
        }

        if (tc.Equals("a", StringComparison.CurrentCultureIgnoreCase))
        {
            if (pa < 20) return cc * 0.03m;
            if (pa == 20) return cc * 0.05m;
            return cc * 0.10m;
        }
    }

    // NO PERECEDEROS
    if (vol >= 50)
        return cc * 0.10m;
    else
        return cc * 0.20m;
}


decimal CalculateDepreciation(int pa)
{
    return pa < 30 ? 0.95m : 0.85m;
}


decimal CalculateExhibitionCost(string tp, string tc, string ma, decimal ca)
{
    // PERECEDEROS
    if (tp.Equals("p", StringComparison.CurrentCultureIgnoreCase))
    {
        if (tc.Equals("f", StringComparison.CurrentCultureIgnoreCase))
        {
            if (ma.Equals("n", StringComparison.CurrentCultureIgnoreCase))
                return ca * 2;

            if (ma.Equals("c", StringComparison.CurrentCultureIgnoreCase))
                return ca;
        }
    }

    // NO PERECEDEROS
    if (tp.Equals("n", StringComparison.CurrentCultureIgnoreCase))
    {
        if (ma.Equals("e", StringComparison.CurrentCultureIgnoreCase))
            return ca * 0.05m;

        if (ma.Equals("g", StringComparison.CurrentCultureIgnoreCase))
            return ca * 0.07m;
    }

    return 0;
}

