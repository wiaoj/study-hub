namespace Memento;
public partial class Star {
    private sealed class StarMementoInternal : IStarMemento {
        public StarType Type { get; }
        public Int32 AgeYears { get; }
        public Int32 MassTons { get; }

        public StarMementoInternal(StarType type, Int32 ageYears, Int32 massTons) {
            this.Type = type;
            this.AgeYears = ageYears;
            this.MassTons = massTons; 
        }
    }
}