using DepreciationCalculator.Models;
using DepreciationCalculator.Validators;

namespace DepreciationCalculator.UI
{
    /// <summary>
    /// Clase responsable de la interacción con el usuario.
    /// Maneja la captura de datos y presentación de resultados.
    /// </summary>
    public class UserInterface
    {
        /// <summary>
        /// Captura los datos del activo desde la entrada del usuario.
        /// Valida cada entrada antes de proceder.
        /// </summary>
        /// <returns>Objeto AssetDepreciation con los datos ingresados</returns>
        public static AssetDepreciation GetAssetInputFromUser()
        {
            Console.Clear();
            DisplayHeader();

            decimal originalValue = 0;
            decimal depreciationRate = 0;
            int years = 0;

            // Obtener valor original
            Console.WriteLine("📌 PASO 1: Ingrese el valor de compra original");
            while (true)
            {
                Console.Write("💵 Valor original ($): ");
                string input = Console.ReadLine() ?? string.Empty;

                if (InputValidator.ValidateDecimal(input, out originalValue, out string error))
                {
                    break;
                }

                Console.WriteLine(error);
            }

            Console.WriteLine();

            // Obtener tasa de depreciación
            Console.WriteLine("📌 PASO 2: Ingrese la tasa de depreciación anual (0-100%)");
            while (true)
            {
                Console.Write("📉 Tasa de depreciación (%): ");
                string input = Console.ReadLine() ?? string.Empty;

                if (InputValidator.ValidateDepreciationRate(input, out depreciationRate, out string error))
                {
                    break;
                }

                Console.WriteLine(error);
            }

            Console.WriteLine();

            // Obtener años
            Console.WriteLine("📌 PASO 3: Ingrese el número de años a evaluar");
            while (true)
            {
                Console.Write("📅 Años: ");
                string input = Console.ReadLine() ?? string.Empty;

                if (InputValidator.ValidateYears(input, out years, out string error))
                {
                    break;
                }

                Console.WriteLine(error);
            }

            return new AssetDepreciation(originalValue, depreciationRate, years);
        }

        /// <summary>
        /// Muestra el encabezado del programa.
        /// </summary>
        public static void DisplayHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   CALCULADORA DE DEPRECIACIÓN POR SALDO DECRECIENTE    ║");
            Console.WriteLine("║                     Sistema de Activos                 ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Muestra un menú de opciones al usuario.
        /// </summary>
        /// <returns>La opción seleccionada por el usuario</returns>
        public static char ShowMainMenu()
        {
            Console.WriteLine("\n┌─────────────────────────────────────────────────────┐");
            Console.WriteLine("│               MENÚ DE OPCIONES                      │");
            Console.WriteLine("├─────────────────────────────────────────────────────┤");
            Console.WriteLine("│  1️⃣  Calcular depreciación de un activo              │");
            Console.WriteLine("│  2️⃣  Ver ejemplo predefinido                        │");
            Console.WriteLine("│  3️⃣  Salir                                           │");
            Console.WriteLine("└─────────────────────────────────────────────────────┘");
            Console.Write("\n👉 Seleccione una opción (1-3): ");

            return Console.ReadLine()?.FirstOrDefault() ?? '0';
        }

        /// <summary>
        /// Muestra un mensaje de error.
        /// </summary>
        public static void ShowErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Muestra un mensaje de éxito.
        /// </summary>
        public static void ShowSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Pausa la ejecución esperando que el usuario presione una tecla.
        /// </summary>
        public static void PauseExecution()
        {
            Console.WriteLine("\n➡️  Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        /// <summary>
        /// Muestra un ejemplo predefinido (laptop de $1000 con 20% depreciación).
        /// </summary>
        public static AssetDepreciation GetExampleAsset()
        {
            return new AssetDepreciation(
                originalValue: 1000,
                depreciationRate: 20,
                years: 3
            );
        }
    }
}
