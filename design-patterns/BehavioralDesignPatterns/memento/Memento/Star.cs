namespace Memento;
public partial class Star {
    private StarType type;
    private Int32 ageYears;
    private Int32 massTons;

    public Star(StarType startType, Int32 startAge, Int32 startMass) {
        this.type = startType;
        this.ageYears = startAge;
        this.massTons = startMass;
    }

    public void TimePasses() {
        this.ageYears *= 2;
        this.massTons *= 8;

        switch(this.type) {
            case StarType.RED_GIANT:
                this.type = StarType.WHITE_DWARF;
                break;
            case StarType.SUN:
                this.type = StarType.RED_GIANT;
                break;
            case StarType.SUPERNOVA:
                this.type = StarType.DEAD;
                break;
            case StarType.WHITE_DWARF:
                this.type = StarType.SUPERNOVA;
                break;
            case StarType.DEAD:
                this.ageYears *= 2;
                this.massTons = 0;
                break;
        }
    }

    public IStarMemento GetMemento() {
        return new StarMementoInternal(this.type, this.ageYears, this.massTons);
    }

    public void SetMemento(IStarMemento memento) {
        if(memento is StarMementoInternal state) {
            this.type = state.Type;
            this.ageYears = state.AgeYears;
            this.massTons = state.MassTons;
        }
    }

    public override String ToString() {
        return $"{this.type.Title()} age: {this.ageYears} years mass: {this.massTons} tons";
    }
}