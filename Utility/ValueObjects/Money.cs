using System;
using System.Collections.Generic;

namespace Utility.ValueObjects
{
    public class Money : ValueObject
    {
        public Currency Currency { get; }
        public long SubunitsAmount { get; }

        public long UnitsAmount
            => SubunitsAmount / Currency.SubunitsPerUnit;
        public decimal Amount
            => (decimal)SubunitsAmount / Currency.SubunitsPerUnit;

        public bool Negative
            => SubunitsAmount < 0;
        public ulong SubunitsAmountAbs
            => (Negative) ? (ulong)(SubunitsAmount * -1) : (ulong)SubunitsAmount;
        public ulong UnitsAmountAbs
            => SubunitsAmountAbs / Currency.SubunitsPerUnit;
        public uint SubunitsRemainderAbs
            => (uint)(SubunitsAmountAbs - (UnitsAmountAbs * Currency.SubunitsPerUnit));
        public decimal SubunitsRemainderAbsFraction
            => Math.Round((decimal)SubunitsRemainderAbs / Currency.SubunitsPerUnit, (int)Currency.FractionalDigits);

        public Money(Currency currency, long subunitsAmount)
        {
            Currency = currency;
            SubunitsAmount = subunitsAmount;
        }

        public Money(Currency currency, decimal amount)
            : this(currency, (long)Math.Round(amount * currency.SubunitsPerUnit))
        { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Currency;
            yield return SubunitsAmount;
        }

        public override string ToString()
        {
            string s = null;
            if (Negative)
                s += "-";
            int subunitComponent = (int)(SubunitsRemainderAbsFraction * (decimal)Math.Pow(10, Currency.FractionalDigits));
            s += Currency.Symbol + UnitsAmountAbs + "." + subunitComponent.ToString("D" + Currency.FractionalDigits) + Currency.Code;
            return s;
        }

        #region Operator overloads

        public static bool operator <(Money first, Money second)
        {
            if (!first.Currency.Equals(second.Currency))
                throw new ArgumentException("Cannot apply relational operators to money with different currencies.");
            return first.SubunitsAmount < second.SubunitsAmount;
        }
        public static bool operator >=(Money first, Money second)
        {
            return !(first < second);
        }

        public static bool operator >(Money first, Money second)
        {
            if (!first.Currency.Equals(second.Currency))
                throw new ArgumentException("Cannot apply relational operators to money with different currencies.");
            return first.SubunitsAmount > second.SubunitsAmount;
        }

        public static bool operator <=(Money first, Money second)
        {
            return !(first > second);
        }

        public static Money operator +(Money first, Money second)
        {
            if (first is null && second is null)
                return null;
            if (first is null)
                return second;
            if (second is null)
                return first;
            if (!first.Currency.Equals(second.Currency))
                throw new ArgumentException("Cannot add money with different currencies.");
            return new Money(first.Currency, first.SubunitsAmount + second.SubunitsAmount);
        }

        public static Money operator -(Money first, Money second)
        {
            if (first is null && second is null)
                return null;
            if (first is null)
                return new Money(second.Currency, second.Amount * -1);
            if (second is null)
                return first;
            if (!first.Currency.Equals(second.Currency))
                throw new ArgumentException("Cannot subtract money with different currencies.");
            return new Money(first.Currency, first.SubunitsAmount - second.SubunitsAmount);
        }

        public static Money operator *(Money money, decimal multiplier)
        {
            return new Money(money.Currency, (long)Math.Round(money.SubunitsAmount * multiplier));
        }

        public static Money operator *(decimal multiplier, Money money)
        {
            return money * multiplier;
        }

        public static Money operator /(Money money, decimal divisor)
        {
            return new Money(money.Currency, (long)Math.Round(money.SubunitsAmount / divisor));
        }

        #endregion
    }
}
