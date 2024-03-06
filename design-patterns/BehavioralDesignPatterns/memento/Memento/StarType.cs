namespace Memento;
public enum StarType {
    SUN,
    RED_GIANT,
    WHITE_DWARF,
    SUPERNOVA,
    DEAD
}

public static class StartTypeExtensions {
    public static String Title(this StarType type) {
        return type switch {
            StarType.SUN => "Sun",
            StarType.RED_GIANT => "Red giant",
            StarType.WHITE_DWARF => "White dwarf",
            StarType.SUPERNOVA => "Supernova",
            StarType.DEAD => "Dead star",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}