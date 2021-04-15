using System;
using System.Collections.Generic;

namespace Utility.ValueObjects
{
    public class Currency : ValueObject
    {
        #region Static readonly currencies

        public static readonly Currency AUD = new Currency("Australia", "AUD", "$", "Dollar", "Cent", 100);
        public static readonly Currency BRL = new Currency("Brazil", "BRL", "R$", "Real", "Centavo", 100);
        public static readonly Currency CAD = new Currency("Canada", "CAD", "$", "Dollar", "Cent", 100);
        public static readonly Currency CNY = new Currency("China", "CNY", "¥", "Yuan", "Fen", 100);
        public static readonly Currency EUR = new Currency("European Union", "EUR", "€", "Euro", "Cent", 100);
        public static readonly Currency INR = new Currency("India", "INR", "₹", "Rupee", "Paisa", 100);
        public static readonly Currency NGN = new Currency("Nigeria", "NGN", "₦", "Naira", "Kobo", 100);
        public static readonly Currency USD = new Currency("United States of America", "USD", "$", "Dollar", "Cent", 100);

        #endregion

        public string Country { get; }
        public string Code { get; }
        public string Symbol { get; }
        public string UnitName { get; }
        public string SubunitName { get; }
        public uint SubunitsPerUnit { get; }

        public uint FractionalDigits => (uint)Math.Ceiling(Math.Log10(SubunitsPerUnit));

        public Currency(string country, string code, string symbol, string unitName, string subunitName, uint subunitsPerUnit)
        {
            if (subunitsPerUnit < 1)
                throw new ArgumentException("Subunits per unit must be at least 1.", nameof(subunitsPerUnit));

            Country = country;
            Code = code;
            Symbol = symbol;
            UnitName = unitName;
            SubunitName = subunitName;
            SubunitsPerUnit = subunitsPerUnit;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Country;
            yield return Code;
            yield return Symbol;
            yield return UnitName;
            yield return SubunitName;
            yield return SubunitsPerUnit;
        }

        public override string ToString()
        {
            return Symbol + Code;
        }
    }
}
