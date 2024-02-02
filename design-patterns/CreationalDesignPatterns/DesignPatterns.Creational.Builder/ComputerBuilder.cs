using DesignPatterns.Creational.Builder.Domain;

namespace DesignPatterns.Creational.Builder;
public sealed class ComputerBuilder : IComputerBuilder {
    Computer IComputerBuilder.BuildComputer() {
        throw new NotImplementedException();
    }

    CPU IComputerBuilder.BuildCpu() {
        throw new NotImplementedException();
    }

    Display IComputerBuilder.BuildDisplay() {
        throw new NotImplementedException();
    }

    GraphicCard IComputerBuilder.BuildGraphicCard() {
        throw new NotImplementedException();
    }

    HardDrive IComputerBuilder.BuildHardDrive() {
        throw new NotImplementedException();
    }

    RAM IComputerBuilder.BuildRam() {
        throw new NotImplementedException();
    }
}  