using DesignPatterns.Creational.Builder.Domain;

namespace DesignPatterns.Creational.Builder;
internal interface IComputerBuilder {
    RAM BuildRam();
    CPU BuildCpu();
    HardDrive BuildHardDrive();
    GraphicCard BuildGraphicCard();
    Display BuildDisplay();
    Computer BuildComputer();
}