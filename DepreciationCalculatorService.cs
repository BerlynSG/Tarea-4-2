using DepreciationCalculator.Models;

namespace DepreciationCalculator.Services
{
    /// <summary>
    /// Servicio que realiza los cálculos de depreciación por saldo decreciente.
    /// Implementa la lógica recursiva para determinar el valor residual de un activo.
    /// </summary>
    public class DepreciationCalculatorService
    {
        /// <summary>
        /// Calcula el valor residual de un activo después de t años usando recursión.
        /// 
        /// Caso Base: Si t = 0, el valor es igual al valor original (V₀).
        /// Caso Recursivo: Si t > 0, V(t) = V(t-1) × (1 - d/100).
        /// 
        /// La recursión modelará cómo el valor de cada año depende del año anterior.
        /// </summary>
        /// <param name="originalValue">Valor original del activo</param>
        /// <param name="retentionFactor">Factor de retención (1 - d/100)</param>
        /// <param name="years">Años transcurridos</param>
        /// <returns>Valor residual del activo después de t años</returns>
        /// <exception cref="ArgumentException">Si los parámetros son inválidos</exception>
        public decimal CalculateResidualValue(decimal originalValue, decimal retentionFactor, int years)
        {
            // Validar parámetros
            if (originalValue <= 0)
                throw new ArgumentException("El valor original debe ser mayor a 0.", nameof(originalValue));

            if (retentionFactor < 0 || retentionFactor > 1)
                throw new ArgumentException("El factor de retención debe estar entre 0 y 1.", nameof(retentionFactor));

            if (years < 0)
                throw new ArgumentException("Los años no pueden ser negativos.", nameof(years));

            // Llamar al método recursivo privado
            return CalculateRecursive(originalValue, retentionFactor, years);
        }

        /// <summary>
        /// Método privado recursivo que calcula el valor residual.
        /// Este método implementa la lógica recursiva del saldo decreciente.
        /// </summary>
        /// <param name="value">Valor actual del activo</param>
        /// <param name="retentionFactor">Factor de retención</param>
        /// <param name="remainingYears">Años restantes a calcular</param>
        /// <returns>Valor residual calculado recursivamente</returns>
        private decimal CalculateRecursive(decimal value, decimal retentionFactor, int remainingYears)
        {
            // CASO BASE: Si no hay años restantes, retornar el valor actual
            if (remainingYears == 0)
            {
                return value;
            }

            // CASO RECURSIVO: Calcular el valor del siguiente año y restar 1 del contador
            // V(t) = V(t-1) × (1 - d/100)
            return CalculateRecursive(value * retentionFactor, retentionFactor, remainingYears - 1);
        }

        /// <summary>
        /// Genera un reporte detallado del cálculo de depreciación año por año.
        /// </summary>
        /// <param name="asset">El activo a depreciar</param>
        /// <returns>String con el reporte año a año</returns>
        public string GenerateDepreciationReport(AssetDepreciation asset)
        {
            var report = new System.Text.StringBuilder();

            report.AppendLine("\n╔════════════════════════════════════════════════════════╗");
            report.AppendLine("║          REPORTE DE DEPRECIACIÓN POR SALDO            ║");
            report.AppendLine("║                   DECRECIENTE                        ║");
            report.AppendLine("╚════════════════════════════════════════════════════════╝\n");

            report.AppendLine($"📦 Activo: Equipo/Maquinaria");
            report.AppendLine($"💰 Valor Original:        ${asset.OriginalValue:F2}");
            report.AppendLine($"📉 Tasa Depreciación:     {asset.DepreciationRate}%");
            report.AppendLine($"📅 Años a Evaluar:        {asset.Years}");
            report.AppendLine($"🔄 Factor Retención (r):  {asset.RetentionFactor:F4}\n");

            report.AppendLine("┌─────────┬──────────────────┐");
            report.AppendLine("│  Año    │  Valor Residual  │");
            report.AppendLine("├─────────┼──────────────────┤");

            // Generar línea para cada año
            for (int t = 0; t <= asset.Years; t++)
            {
                decimal residualValue = CalculateResidualValue(asset.OriginalValue, asset.RetentionFactor, t);
                report.AppendLine($"│  {t,4}    │  ${residualValue,14:F2}  │");
            }

            report.AppendLine("└─────────┴──────────────────┘\n");

            decimal finalValue = CalculateResidualValue(asset.OriginalValue, asset.RetentionFactor, asset.Years);
            decimal totalDepreciation = asset.OriginalValue - finalValue;
            decimal percentageDepreciated = (totalDepreciation / asset.OriginalValue) * 100;

            report.AppendLine($"📊 RESUMEN FINAL:");
            report.AppendLine($"   • Valor Inicial:         ${asset.OriginalValue:F2}");
            report.AppendLine($"   • Valor Final ({asset.Years} años):      ${finalValue:F2}");
            report.AppendLine($"   • Depreciación Total:    ${totalDepreciation:F2}");
            report.AppendLine($"   • Porcentaje Depreciado: {percentageDepreciated:F2}%\n");

            return report.ToString();
        }
    }
}
