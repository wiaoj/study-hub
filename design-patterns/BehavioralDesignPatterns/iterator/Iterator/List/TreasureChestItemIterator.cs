namespace Iterator.List;
public class TreasureChestItemIterator : IIterator<Item> {
    private readonly TreasureChest chest;
    private Int32 idx;
    private readonly ItemTypes type;


    public TreasureChestItemIterator(TreasureChest chest, ItemTypes type) {
        this.chest = chest;
        this.type = type;
        this.idx = -1;
    }

    public Boolean HasNext() {
        return FindNextIdx() != -1;
    }

    public Item? Next() {
        this.idx = FindNextIdx();
        return this.idx != -1 ? this.chest.Items[this.idx] : default;
    }

    private Int32 FindNextIdx() {
        IReadOnlyList<Item> items = this.chest.Items;
        Int32 tempIdx = this.idx;
        while(true) {
            tempIdx++;
            if(tempIdx >= items.Count) {
                tempIdx = -1;
                break;
            }

            if(this.type == ItemTypes.ANY || items[tempIdx].Type == this.type)
                break;
        }
        return tempIdx;
    }
}