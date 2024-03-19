namespace Ambassador;
public class Client {
    private readonly ServiceAmbassador serviceAmbassador = new();

    public Int64 UseService(Int32 value) {
        Int64 result = this.serviceAmbassador.DoRemoteFunction(value);
        return result;
    }
}