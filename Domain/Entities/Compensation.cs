using Forge.Domain.Common;

namespace Forge.Domain.Entities
{

    public enum CompensationType
    {
        Salary,
        Allowance,
        Commission,
        Stipend
    }

    public readonly record struct Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount cannot be negative.", nameof(amount));
            }

            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));
            }

            Amount = amount;
            Currency = currency;
        }

        public Money Add(Money other)
        {
            if (Currency != other.Currency)
            {
                throw new InvalidOperationException("Cannot add amounts with different currencies.");
            }

            return new Money(Amount + other.Amount, Currency);
        }
    }


    public class Compensation : Entity
    {
        public CompensationType Type { get; private set; }
        public Money Money { get; private set; }
        public DateTime EffectiveDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool IsActive => !EndDate.HasValue || EndDate.Value > DateTime.UtcNow;

        public Compensation(CompensationType type, Money money, DateTime? effectiveDate = null, DateTime? endDate = null)
        {
            SetType(type);
            SetMoney(money);
            SetEffectiveDate(effectiveDate ?? DateTime.UtcNow);
            SetEndDate(endDate);
        }

        private void SetType(CompensationType type)
        {
            if (!Enum.IsDefined(typeof(CompensationType), type))
            {
                throw new ArgumentException("Invalid compensation type.", nameof(type));
            }

            Type = type;
        }

        private void SetMoney(Money money)
        {
            Money = money;
        }

        private void SetEffectiveDate(DateTime effectiveDate)
        {
            EffectiveDate = effectiveDate;
        }

        private void SetEndDate(DateTime? endDate)
        {
            if (endDate.HasValue && endDate.Value < EffectiveDate)
            {
                throw new ArgumentException("End date cannot be before the effective date.", nameof(endDate));
            }

            EndDate = endDate;
        }

        public void End(DateTime endDate)
        {
            SetEndDate(endDate);
        }
    }
}
