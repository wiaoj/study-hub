namespace SmartEnum;
public abstract class CreditCard(Int32 value, String name) : Enumeration<CreditCard>(value, name) {
    public static readonly CreditCard Standart = new StandartCreditCard();
    public static readonly CreditCard Premium = new PremiumCreditCard();
    public static readonly CreditCard Platinum = new PlatinumCreditCard();

    public abstract Double Discount { get; }

    private sealed class StandartCreditCard() : CreditCard(1, nameof(Standart)) {
        public override Double Discount => 0.01D;
    }

    private sealed class PremiumCreditCard() : CreditCard(2, nameof(Premium)) {
        public override Double Discount => 0.05D;
    }

    private sealed class PlatinumCreditCard() : CreditCard(3, nameof(Platinum)) {
        public override Double Discount => 0.1D;
    }
}