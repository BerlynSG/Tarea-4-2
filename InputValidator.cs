namespace DepreciationCalculator.Validators
{
    /// <summary>
    /// Clase responsable de validar todas las entradas del usuario.
    /// Implementa validaciones para valores numéricos y restricciones de rango.
    /// </summary>
    public class InputValidator
    {
        /// <summary>
        /// Valida que un valor sea un número decimal positivo.
        /// </summary>
        /// <param name="input">Entrada del usuario como string</param>
        /// <param name="value">Valor decimal parseado (por referencia)</param>
        /// <param name="errorMessage">Mensaje de error si falla la validación</param>
        /// <returns>true si es válido, false en caso contrario</returns>
        public static bool ValidateDecimal(string input, out decimal value, out string errorMessage)
        {
            value = 0;
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                errorMessage = "❌ Error: La entrada no puede estar vacía.";
                return false;
            }

            if (!decimal.TryParse(input, out value))
            {
                errorMessage = "❌ Error: Ingrese un número válido.";
                return false;
            }

            if (value <= 0)
            {
                errorMessage = "❌ Error: El valor debe ser mayor a 0.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida la tasa de depreciación (debe estar entre 0 y 100).
        /// </summary>
        /// <param name="input">Entrada del usuario como string</param>
        /// <param name="rate">Tasa parseada (por referencia)</param>
        /// <param name="errorMessage">Mensaje de error si falla la validación</param>
        /// <returns>true si es válido, false en caso contrario</returns>
        public static bool ValidateDepreciationRate(string input, out decimal rate, out string errorMessage)
        {
            rate = 0;
            errorMessage = string.Empty;

            if (!ValidateDecimal(input, out decimal value, out errorMessage))
            {
                return false;
            }

            if (value > 100)
            {
                errorMessage = "❌ Error: La tasa de depreciación no puede superar 100%.";
                return false;
            }

            rate = value;
            return true;
        }

        /// <summary>
        /// Valida que el número de años sea un entero no negativo.
        /// </summary>
        /// <param name="input">Entrada del usuario como string</param>
        /// <param name="years">Número de años parseado (por referencia)</param>
        /// <param name="errorMessage">Mensaje de error si falla la validación</param>
        /// <returns>true si es válido, false en caso contrario</returns>
        public static bool ValidateYears(string input, out int years, out string errorMessage)
        {
            years = 0;
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                errorMessage = "❌ Error: La entrada no puede estar vacía.";
                return false;
            }

            if (!int.TryParse(input, out int value))
            {
                errorMessage = "❌ Error: Ingrese un número entero válido.";
                return false;
            }

            if (value < 0)
            {
                errorMessage = "❌ Error: Los años no pueden ser negativos.";
                return false;
            }

            years = value;
            return true;
        }
    }
}
