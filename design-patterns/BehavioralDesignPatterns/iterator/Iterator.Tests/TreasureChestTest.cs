using Iterator.List;
using Xunit.Abstractions;

namespace Iterator.Tests;
public class TreasureChestTest(ITestOutputHelper output) {
    [Theory]
    [MemberData(nameof(TestDatas))]
    public void TestIterator(Item expectedItem) {
        TreasureChest chest = new();
        IIterator<Item> iterator = chest.Iterator(expectedItem.Type);
        Assert.NotNull(iterator);

        while(iterator.HasNext()) {
            Item? item = iterator.Next();
            Assert.NotNull(item);
            Assert.Equal(expectedItem.Type, item.Type);

            String name = item.ToString();
            Assert.NotNull(name);

            if(expectedItem.ToString() == name) {
                output.WriteLine($"Found item: {name}");
                return;
            }
        }

        Assert.Fail($"Expected to find item [{expectedItem}] using iterator, but we didn't.");
    }

    [Theory]
    [MemberData(nameof(TestDatas))]
    public void TestItems(Item expectedItem) {
        TreasureChest chest = new();
        IReadOnlyList<Item> items = chest.Items;
        Assert.NotNull(items);

        foreach(Item item in items) {
            Assert.NotNull(item);
            Assert.NotNull(item.ToString());

            Boolean sameType = expectedItem.Type == item.Type;
            Boolean sameName = expectedItem.ToString() == item.ToString();

            if(sameType && sameName) {
                output.WriteLine($"Found item: {item}");
                return;
            }
        }

        Assert.Fail($"Expected to find item [{expectedItem}] in the item list, but we didn't.");
    }

    [Theory]
    [InlineData(ItemTypes.RING)]
    [InlineData(ItemTypes.POTION)]
    [InlineData(ItemTypes.WEAPON)]
    [InlineData(ItemTypes.ANY)]
    public void TestTreasureChestIteratorForType(ItemTypes itemType) {
        TreasureChest treasureChest = new();
        IIterator<Item> itemIterator = treasureChest.Iterator(itemType);
        while(itemIterator.HasNext()) {
            Item? item = itemIterator.Next();
            Assert.NotNull(item);
            Assert.True(item.Type == itemType || itemType == ItemTypes.ANY);
            output.WriteLine(item.ToString());
        }
    }

    public static TheoryData<Item> TestDatas() {
        return [
            new(ItemTypes.POTION, "Elixir of Bravery"),
            new(ItemTypes.RING, "Shadow Band"),
            new(ItemTypes.POTION, "Wisdom Brew"),
            new(ItemTypes.POTION, "Blood Essence"),
            new(ItemTypes.WEAPON, "Silver Blade +1"),
            new(ItemTypes.POTION, "Decay Elixir"),
            new(ItemTypes.POTION, "Healing Salve"),
            new(ItemTypes.RING, "Armor Ringlet"),
            new(ItemTypes.WEAPON, "Halberd of Steel"),
            new(ItemTypes.WEAPON, "Poisoned Dagger")
        ];
    }
}
