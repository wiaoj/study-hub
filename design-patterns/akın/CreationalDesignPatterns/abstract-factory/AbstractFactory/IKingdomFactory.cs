namespace AbstractFactory;
public interface IKingdomFactory {
    ICastle CreateCastle();
    IKing CreateKing();
    IArmy CreateArmy();
}