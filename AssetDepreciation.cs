namespace DepreciationCalculator.Models
{
    /// <summary>
    /// Clase que representa un activo con información de depreciación.
    /// Modela el comportamiento de saldo decreciente para calcular el valor residual.
    /// </summary>
    public class AssetDepreciation
    {
        /// <summary>
        /// Valor de compra original del activo (en unidades monetarias).
        /// </summary>
        public decimal OriginalValue { get; set; }

        /// <summary>
        /// Tasa de depreciación anual como porcentaje (0-100).
        /// </summary>
        public decimal DepreciationRate { get; set; }

        /// <summary>
        /// Años de vida útil transcurridos.
        /// </summary>
        public int Years { get; set; }

        /// <summary>
        /// Factor de retención de valor (1 - d/100).
        /// Se calcula como: 1 - (DepreciationRate / 100)
        /// </summary>
        public decimal RetentionFactor
        {
            get { return 1 - (DepreciationRate / 100); }
        }

        /// <summary>
        /// Constructor para inicializar un activo con depreciación.
        /// </summary>
        /// <param name="originalValue">Valor original del activo</param>
        /// <param name="depreciationRate">Tasa de depreciación anual</param>
        /// <param name="years">Años transcurridos</param>
        public AssetDepreciation(decimal originalValue, decimal depreciationRate, int years)
        {
            OriginalValue = originalValue;
            DepreciationRate = depreciationRate;
            Years = years;
        }

        /// <summary>
        /// Retorna una descripción formateada del activo.
        /// </summary>
        public override string ToString()
        {
            return $"Activo - Valor Original: ${OriginalValue:F2}, " +
                   $"Tasa Depreciación: {DepreciationRate}%, " +
                   $"Años: {Years}";
        }
    }
}
