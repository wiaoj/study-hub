namespace DesingPatterns.Structural.Proxy.RealWorldMath;
public class Math : IMath {
    public Double Add(Double x, Double y) {
        return x + y;
    }

    public Double Div(Double x, Double y) {
        return x / y;
    }

    public Double Mul(Double x, Double y) {
        return x * y;
    }

    public Double Sub(Double x, Double y) {
        return x - y;
    }
}