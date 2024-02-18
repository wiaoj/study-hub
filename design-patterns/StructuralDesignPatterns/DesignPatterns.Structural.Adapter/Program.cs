using DesignPatterns.Structural.Adapter;

var captain = new Captain(new FishingBoatAdapter());
captain.Row();