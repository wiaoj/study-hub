namespace Factory;
public static class CoinFactory {
    public enum CoinTypes {
        COPPER,
        GOLD
    }

    public static ICoin CreateCoin(CoinTypes type) {
        return type switch {
            CoinTypes.COPPER => new CopperCoin(),
            CoinTypes.GOLD => new GoldCoin(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}