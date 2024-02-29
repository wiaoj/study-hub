using DesignPatterns.Structural.Adapter;

Captain captain = new(new FishingBoatAdapter());
captain.Row();
