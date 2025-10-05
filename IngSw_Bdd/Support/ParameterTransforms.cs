using System;
using System.Globalization;
using Reqnroll;

namespace IngSw_Bdd.Support
{
    [Binding]
    public sealed class ParameterTransforms
    {
        [StepArgumentTransformation(@"-?\d+")]
        public int TransformEntero(string valor)
        {
            return int.Parse(valor, NumberStyles.Integer, CultureInfo.InvariantCulture);
        }

        [StepArgumentTransformation(@"-?\d+(?:[\.,]\d+)?")]
        public decimal TransformDecimal(string valor)
        {
            var normalized = (valor ?? string.Empty).Replace(',', '.');
            return decimal.Parse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture);
        }
    }
}


