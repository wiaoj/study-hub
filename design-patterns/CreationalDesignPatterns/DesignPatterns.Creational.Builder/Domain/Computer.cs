namespace DesignPatterns.Creational.Builder.Domain;
internal class Computer {
    public String Name { get; set; }
    public CPU CPU { get; set; }
    public RAM RAM { get; set; }
    public HardDrive HardDrive { get; set; }
    public GraphicCard GraphicCard { get; set; }
    public Display Display { get; set; }
    public Keyboard Keyboard { get; set; }
    public Mouse Mouse { get; set; }


    public Computer() { }
    public Computer(String name,
                    CPU cpu,
                    RAM ram,
                    HardDrive hardDrive,
                    GraphicCard graphicCard) {
        this.Name = name;
        this.CPU = cpu;
        this.RAM = ram;
        this.HardDrive = hardDrive;
        this.GraphicCard = graphicCard;
    }

    public Computer(String name,
                    CPU cpu,
                    RAM ram,
                    HardDrive hardDrive,
                    GraphicCard graphicCard,
                    Display display) : this(name, cpu, ram, hardDrive, graphicCard) {
        this.Display = display;
    }

    public Computer(String name,
                    CPU cpu,
                    RAM ram,
                    HardDrive hardDrive,
                    GraphicCard graphicCard,
                    Display display,
                    Keyboard keyboard) : this(name, cpu, ram, hardDrive, graphicCard, display) {
        this.Keyboard = keyboard;
    }

    public Computer(String name,
                    CPU cpu,
                    RAM ram,
                    HardDrive hardDrive,
                    GraphicCard graphicCard,
                    Display display,
                    Keyboard keyboard,
                    Mouse mouse) : this(name, cpu, ram, hardDrive, graphicCard, display, keyboard) {
        this.Mouse = mouse;
    }

    public void Start() { }
}