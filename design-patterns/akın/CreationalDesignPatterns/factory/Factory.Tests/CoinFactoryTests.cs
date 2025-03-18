using static Factory.CoinFactory;

namespace Factory.Tests;
public class CoinFactoryTests {
    [Fact]
    public void ShouldReturnCopperCoinInstance() {
        ICoin copperCoin = CoinFactory.CreateCoin(CoinTypes.COPPER);
        Assert.IsType<CopperCoin>(copperCoin);
    }

    [Fact]
    public void ShouldReturnGoldCoinInstance() {
        ICoin goldCoin = CoinFactory.CreateCoin(CoinTypes.GOLD);
        Assert.IsType<GoldCoin>(goldCoin);
    }
}