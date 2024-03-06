namespace Memento.Tests;
public class StarTests {
    [Fact]
    public void TestTimePasses() {
        Star star = new(StarType.SUN, 1, 2);
        Assert.Equal("Sun age: 1 years mass: 2 tons", star.ToString());

        star.TimePasses();
        Assert.Equal("Red giant age: 2 years mass: 16 tons", star.ToString());

        star.TimePasses();
        Assert.Equal("White dwarf age: 4 years mass: 128 tons", star.ToString());

        star.TimePasses();
        Assert.Equal("Supernova age: 8 years mass: 1024 tons", star.ToString());

        star.TimePasses();
        Assert.Equal("Dead star age: 16 years mass: 8192 tons", star.ToString());

        star.TimePasses();
        Assert.Equal("Dead star age: 64 years mass: 0 tons", star.ToString());

        star.TimePasses();
        Assert.Equal("Dead star age: 256 years mass: 0 tons", star.ToString());
    }

    [Fact]
    public void TestSetMemento() {
        Star star = new(StarType.SUN, 1, 2);
        IStarMemento firstMemento = star.GetMemento();
        Assert.Equal("Sun age: 1 years mass: 2 tons", star.ToString());

        star.TimePasses();
        IStarMemento secondMemento = star.GetMemento();
        Assert.Equal("Red giant age: 2 years mass: 16 tons", star.ToString());

        star.TimePasses();
        IStarMemento thirdMemento = star.GetMemento();
        Assert.Equal("White dwarf age: 4 years mass: 128 tons", star.ToString());

        star.TimePasses();
        Assert.Equal("Supernova age: 8 years mass: 1024 tons", star.ToString());

        star.SetMemento(thirdMemento);
        Assert.Equal("White dwarf age: 4 years mass: 128 tons", star.ToString());

        star.TimePasses();
        Assert.Equal("Supernova age: 8 years mass: 1024 tons", star.ToString());

        star.SetMemento(secondMemento);
        Assert.Equal("Red giant age: 2 years mass: 16 tons", star.ToString());

        star.SetMemento(firstMemento);
        Assert.Equal("Sun age: 1 years mass: 2 tons", star.ToString());
    }
}