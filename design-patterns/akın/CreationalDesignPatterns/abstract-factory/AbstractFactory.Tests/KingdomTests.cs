using AbstractFactory.Elf;
using AbstractFactory.Human;
using AbstractFactory.Orc;
using Xunit.Abstractions;
using static AbstractFactory.Kingdom.FactoryMaker;

namespace AbstractFactory.Tests;
public class KingdomTests(ITestOutputHelper output) {

    [Theory]
    [InlineData(KingdomTypes.ELF, typeof(ElfKing), AbstractFactoryConstants.Elf.KingName)]
    [InlineData(KingdomTypes.HUMAN, typeof(HumanKing), AbstractFactoryConstants.Human.KingName)]
    [InlineData(KingdomTypes.ORC, typeof(OrcKing), AbstractFactoryConstants.Orc.KingName)]
    public void VerifyKingCreation(KingdomTypes kingdomType, Type expectedType, String expectedName) {
        Kingdom kingdom = CreateKingdom(kingdomType);

        Assert.IsType(expectedType, kingdom.King);
        output.WriteLine($"Verified king type for {kingdomType}: {kingdom.King.GetType().Name} is as expected: {expectedType.Name}.");

        Assert.Equal(expectedName, kingdom.King.Name);
        output.WriteLine($"Verified king name for {kingdomType}: {kingdom.King.Name} is as expected: {expectedName}.");
    }

    [Theory]
    [InlineData(KingdomTypes.ELF, typeof(ElfCastle), AbstractFactoryConstants.Elf.CastleDescription)]
    [InlineData(KingdomTypes.HUMAN, typeof(HumanCastle), AbstractFactoryConstants.Human.CastleDescription)]
    [InlineData(KingdomTypes.ORC, typeof(OrcCastle), AbstractFactoryConstants.Orc.CastleDescription)]
    public void VerifyCastleCreation(KingdomTypes kingdomType, Type expectedType, String expectedDescription) {
        Kingdom kingdom = CreateKingdom(kingdomType);

        Assert.IsType(expectedType, kingdom.Castle);
        output.WriteLine($"Verified castle type for {kingdomType}: {kingdom.Castle.GetType().Name} is as expected: {expectedType.Name}.");

        Assert.Equal(expectedDescription, kingdom.Castle.Description);
        output.WriteLine($"Verified castle description for {kingdomType}: {kingdom.Castle.Description} is as expected: {expectedDescription}.");
    }

    [Theory]
    [InlineData(KingdomTypes.ELF, typeof(ElfKing), AbstractFactoryConstants.Elf.ArmySize)]
    [InlineData(KingdomTypes.HUMAN, typeof(HumanKing), AbstractFactoryConstants.Human.ArmySize)]
    [InlineData(KingdomTypes.ORC, typeof(OrcKing), AbstractFactoryConstants.Orc.ArmySize)]
    public void VerifyArmySize(KingdomTypes kingdomType, Type expectedType, Int32 expectedSize) {
        Kingdom kingdom = CreateKingdom(kingdomType);

        Assert.IsType(expectedType, kingdom.King);
        output.WriteLine($"Verified king type for army size check in {kingdomType}: {kingdom.King.GetType().Name} is as expected: {expectedType.Name}.");

        Assert.Equal(expectedSize, kingdom.Army.Size);
        output.WriteLine($"Verified army size for {kingdomType}: {kingdom.Army.Size} is as expected: {expectedSize}.");
    }

    [Fact]
    public void VerifyElfKingdomCreation() {
        Kingdom kingdom = CreateKingdom(KingdomTypes.ELF);

        Assert.IsType<ElfKing>(kingdom.King);
        output.WriteLine($"Verified Elf King creation: {kingdom.King.GetType().Name} is as expected.");

        Assert.Equal(AbstractFactoryConstants.Elf.KingName, kingdom.King.Name);
        output.WriteLine($"Verified Elf King name: {kingdom.King.Name} is as expected.");

        Assert.IsType<ElfCastle>(kingdom.Castle);
        output.WriteLine($"Verified Elf Castle creation: {kingdom.Castle.GetType().Name} is as expected.");

        Assert.Equal(AbstractFactoryConstants.Elf.CastleDescription, kingdom.Castle.Description);
        output.WriteLine($"Verified Elf Castle description: {kingdom.Castle.Description} is as expected.");

        Assert.IsType<ElfArmy>(kingdom.Army);
        output.WriteLine($"Verified Elf Army creation: {kingdom.Army.GetType().Name} is as expected.");

        Assert.Equal(AbstractFactoryConstants.Elf.ArmySize, kingdom.Army.Size);
        output.WriteLine($"Verified Elf Army size: {kingdom.Army.Size} is as expected.");
    }

    [Fact]
    public void VerifyHumanKingdomCreation() {
        Kingdom kingdom = CreateKingdom(KingdomTypes.HUMAN);

        Assert.IsType<HumanKing>(kingdom.King);
        Assert.Equal(AbstractFactoryConstants.Human.KingName, kingdom.King.Name);
        Assert.IsType<HumanCastle>(kingdom.Castle);
        Assert.Equal(AbstractFactoryConstants.Human.CastleDescription, kingdom.Castle.Description);
        Assert.IsType<HumanArmy>(kingdom.Army);
        Assert.Equal(AbstractFactoryConstants.Human.ArmySize, kingdom.Army.Size);
    }

    [Fact]
    public void VerifyOrcKingdomCreation() {
        Kingdom kingdom = CreateKingdom(KingdomTypes.ORC);

        Assert.IsType<OrcKing>(kingdom.King);
        Assert.Equal(AbstractFactoryConstants.Orc.KingName, kingdom.King.Name);
        Assert.IsType<OrcCastle>(kingdom.Castle);
        Assert.Equal(AbstractFactoryConstants.Orc.CastleDescription, kingdom.Castle.Description);
        Assert.IsType<OrcArmy>(kingdom.Army);
        Assert.Equal(AbstractFactoryConstants.Orc.ArmySize, kingdom.Army.Size);
    }

    private static Kingdom CreateKingdom(KingdomTypes kingdomType) {
        IKingdomFactory kingdomFactory = MakeFactory(kingdomType);
        return new(kingdomFactory.CreateKing(), kingdomFactory.CreateCastle(), kingdomFactory.CreateArmy());
    }
}