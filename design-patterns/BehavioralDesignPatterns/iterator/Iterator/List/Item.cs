namespace Iterator.List;
public class Item {
    private ItemTypes type;
    private readonly String name;

    public ItemTypes Type => this.type;

    public Item(ItemTypes type, String name) {
        SetType(type);
        this.name = name;
    }

    public void SetType(ItemTypes type) {
        this.type = type;
    }

    public override String ToString() {
        return this.name;
    }
}