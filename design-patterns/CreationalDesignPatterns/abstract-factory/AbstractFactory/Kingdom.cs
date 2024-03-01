using AbstractFactory.Elf;
using AbstractFactory.Human;
using AbstractFactory.Orc;

namespace AbstractFactory;
public class Kingdom {
    public IKing King { get; private set; }
    public ICastle Castle { get; private set; }
    public IArmy Army { get; private set; }

    public Kingdom(IKing king, ICastle castle, IArmy army) {
        this.King = king;
        this.Castle = castle;
        this.Army = army;
    }

    public static class FactoryMaker {

        public enum KingdomTypes : Byte {
            HUMAN,
            ELF,
            ORC
        }

        public static IKingdomFactory MakeFactory(KingdomTypes type) {
            return type switch {
                KingdomTypes.HUMAN => new HumanKingdomFactory(),
                KingdomTypes.ELF => new ElfKingdomFactory(),
                KingdomTypes.ORC => new OrcKingdomFactory(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}