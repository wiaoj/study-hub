namespace DesignPatterns.Structural.Composite.BasicComposite;
internal static class Test {
    public static void CreateTest() {
        //Creating Leaf Objects or you can say child objects
        IComponent hardDisk = new Leaf("Hard Disk", 2000);
        IComponent ram = new Leaf("RAM", 3000);
        IComponent cpu = new Leaf("CPU", 2000);
        IComponent mouse = new Leaf("Mouse", 2000);
        IComponent keyboard = new Leaf("Keyboard", 2000);

        //Creating Composite Objects
        Composite motherBoard = new("MotherBoard");
        Composite cabinet = new("Cabinet");
        Composite peripherals = new("Peripherals");
        Composite computer = new("Computer");

        //Creating Tree Structure i.e. Adding Child Components inside the Composite Component
        //Adding CPU and RAM in Mother Board
        motherBoard.AddComponent(cpu);
        motherBoard.AddComponent(ram);
        //Adding Mother Board and Hard Disk in Cabinet
        cabinet.AddComponent(motherBoard);
        cabinet.AddComponent(hardDisk);
        //Adding Mouse and Keyboard in peripherals
        peripherals.AddComponent(mouse);
        peripherals.AddComponent(keyboard);
        //Adding Cabinet and Peripherals in Computer
        computer.AddComponent(cabinet);
        computer.AddComponent(peripherals);
        //To Display the Price of the Computer i.e. it will display the Price of all components
        Console.WriteLine("Price of Computer Composite Components");
        computer.DisplayPrice();
        //To display the Price of the Keyboard
        Console.WriteLine("\nPrice of Keyboard Child or Leaf Component:");
        keyboard.DisplayPrice();

        //To display the Price of the Cabinet
        Console.WriteLine("\nPrice of Cabinet Composite Component:");
        cabinet.DisplayPrice();
    }
}