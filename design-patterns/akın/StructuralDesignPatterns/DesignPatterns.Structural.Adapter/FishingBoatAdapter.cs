namespace DesignPatterns.Structural.Adapter;
internal sealed class FishingBoatAdapter : IRowingBoat {
    private readonly FishingBoat boat = new();

    public void Row() {
        this.boat.Sail();
    }
}