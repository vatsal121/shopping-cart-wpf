using System;
using System.Collections.Generic;

namespace Utility.ValueObjects
{
    public class Weight : ValueObject
    {
        public const decimal GramsPerKilogram = 1000;
        public const decimal GramsPerOunce = 28.34952312m;
        public const decimal GramsPerPound = 453.59236992m;

        public decimal Grams { get; }
        public decimal Kilograms => GramsToKilograms(Grams);
        public decimal Ounces => GramsToOunces(Grams);
        public decimal Pounds => GramsToPounds(Grams);

        public Weight(decimal grams)
        {
            if (grams < 0)
                throw new ArgumentOutOfRangeException(nameof(grams), "Weight cannot be negative");
            Grams = grams;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Grams;
        }

        public override string ToString()
        {
            return $"{Grams}" + ((Grams == 1) ? " gram" : " grams");
        }

        #region Static conversion methods

        public static Weight CreateWeightInKilograms(decimal kilograms) => new Weight(KilogramsToGrams(kilograms));
        public static Weight CreateWeightInOunces(decimal ounces) => new Weight(OuncesToGrams(ounces));
        public static Weight CreateWeightInPounds(decimal pounds) => new Weight(PoundsToGrams(pounds));

        public static decimal KilogramsToGrams(decimal kilograms) => kilograms * GramsPerKilogram;
        public static decimal OuncesToGrams(decimal ounces) => ounces * GramsPerOunce;
        public static decimal PoundsToGrams(decimal pounds) => pounds * GramsPerPound;

        public static decimal GramsToKilograms(decimal grams) => grams / GramsPerKilogram;
        public static decimal GramsToOunces(decimal grams) => grams / GramsPerOunce;
        public static decimal GramsToPounds(decimal grams) => grams / GramsPerPound;

        #endregion

        #region Operator overloads

        public static bool operator <(Weight first, Weight second)
        {
            return first.Grams < second.Grams;
        }

        public static bool operator >=(Weight first, Weight second)
        {
            return !(first < second);
        }

        public static bool operator >(Weight first, Weight second)
        {
            return first.Grams > second.Grams;
        }

        public static bool operator <=(Weight first, Weight second)
        {
            return !(first > second);
        }

        public static Weight operator +(Weight first, Weight second)
        {
            if (first is null)
                return second;
            if (second is null)
                return first;
            return new Weight(first.Grams + second.Grams);
        }

        public static Weight operator -(Weight first, Weight second)
        {
            if (first is null)
                throw new ArgumentNullException(nameof(first), "Cannot subtract a weight from null.");
            if (second is null)
                return first;
            if (second.Grams > first.Grams)
                throw new ArgumentOutOfRangeException(nameof(second), "Cannot subtract a greater weight from a lesser weight.");
            return new Weight(first.Grams - second.Grams);
        }

        public static Weight operator *(Weight weight, decimal multiplier)
        {
            if (multiplier < 0)
                throw new ArgumentOutOfRangeException(nameof(multiplier), "Weight multiplier cannot be negative.");
            return new Weight(weight.Grams * multiplier);
        }

        public static Weight operator *(decimal multiplier, Weight weight)
        {
            return weight * multiplier;
        }

        public static Weight operator /(Weight weight, decimal divisor)
        {
            if (divisor <= 0)
                throw new ArgumentOutOfRangeException(nameof(divisor), "Weight divisor must be greater than 0.");
            return new Weight(weight.Grams / divisor);
        }

        #endregion
    }
}
