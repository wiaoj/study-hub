namespace DesingPatterns.Structural.Proxy.RealWorldMath;
internal class MathProxy : IMath {
    private readonly Math math = new();

    public Double Add(Double x, Double y) {
        return this.math.Add(x, y);
    }

    public Double Div(Double x, Double y) {
        return this.math.Div(x, y);
    }

    public Double Mul(Double x, Double y) {
        return this.math.Mul(x, y);
    }

    public Double Sub(Double x, Double y) {
        return this.math.Sub(x, y);
    }
}