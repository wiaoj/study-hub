namespace DesignPatterns.Structural.Adapter;
public sealed class Captain(IRowingBoat rowingBoat) {
    public void Row() {
        rowingBoat.Row();
    }
}