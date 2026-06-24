using DepreciationCalculator.Services;
using DepreciationCalculator.UI;

/// <summary>
/// Punto de entrada del programa.
/// Implementa un menú interactivo para calcular depreciación de activos.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        var calculatorService = new DepreciationCalculatorService();
        bool running = true;

        while (running)
        {
            UserInterface.DisplayHeader();
            char option = UserInterface.ShowMainMenu();

            switch (option)
            {
                case '1':
                    HandleCustomCalculation(calculatorService);
                    break;

                case '2':
                    HandleExampleCalculation(calculatorService);
                    break;

                case '3':
                    running = false;
                    Console.WriteLine("\n👋 ¡Gracias por usar la calculadora de depreciación! Hasta luego.\n");
                    break;

                default:
                    UserInterface.ShowErrorMessage("Opción inválida. Por favor, seleccione 1, 2 o 3.");
                    UserInterface.PauseExecution();
                    break;
            }
        }
    }

    /// <summary>
    /// Maneja el cálculo personalizado ingresado por el usuario.
    /// </summary>
    static void HandleCustomCalculation(DepreciationCalculatorService calculatorService)
    {
        try
        {
            var asset = UserInterface.GetAssetInputFromUser();

            decimal residualValue = calculatorService.CalculateResidualValue(
                asset.OriginalValue,
                asset.RetentionFactor,
                asset.Years
            );

            Console.Clear();
            UserInterface.DisplayHeader();

            string report = calculatorService.GenerateDepreciationReport(asset);
            Console.WriteLine(report);

            UserInterface.ShowSuccessMessage($"El valor residual después de {asset.Years} años es: ${residualValue:F2}");

            UserInterface.PauseExecution();
        }
        catch (ArgumentException ex)
        {
            UserInterface.ShowErrorMessage(ex.Message);
            UserInterface.PauseExecution();
        }
    }

    /// <summary>
    /// Maneja el cálculo con datos predefinidos de ejemplo.
    /// Ejemplo: Laptop de $1,000 con 20% de depreciación anual.
    /// </summary>
    static void HandleExampleCalculation(DepreciationCalculatorService calculatorService)
    {
        Console.Clear();
        UserInterface.DisplayHeader();

        Console.WriteLine("📚 EJEMPLO PREDEFINIDO:");
        Console.WriteLine("   Activo: Laptop");
        Console.WriteLine("   Valor Original: $1,000.00");
        Console.WriteLine("   Tasa de Depreciación: 20% anual");
        Console.WriteLine("   Años a Evaluar: 3\n");

        var exampleAsset = UserInterface.GetExampleAsset();

        decimal residualValue = calculatorService.CalculateResidualValue(
            exampleAsset.OriginalValue,
            exampleAsset.RetentionFactor,
            exampleAsset.Years
        );

        string report = calculatorService.GenerateDepreciationReport(exampleAsset);
        Console.WriteLine(report);

        UserInterface.ShowSuccessMessage($"El valor residual después de {exampleAsset.Years} años es: ${residualValue:F2}");

        UserInterface.PauseExecution();
    }
}
