namespace Iterator.List;
public class TreasureChest {
    private readonly List<Item> items;
    public IReadOnlyList<Item> Items => this.items.AsReadOnly();

    public TreasureChest() {
        this.items = [
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

    public IIterator<Item> Iterator(ItemTypes itemType) {
        return new TreasureChestItemIterator(this, itemType);
    }
}