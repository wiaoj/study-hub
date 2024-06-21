using System.Reflection;

namespace SmartEnum;
public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>, IEqualityComparer<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum> {
    private static readonly Dictionary<Int32, TEnum> Enumerations = CreateEnumerations();
    public Int32 Value { get; protected init; }
    public String Name { get; protected init; }

    protected Enumeration(Int32 value, String name) {
        this.Value = value;
        this.Name = name;
    }


    public static TEnum? FromValue(Int32 value) {
        return Enumerations.TryGetValue(value, out TEnum? enumeration)
            ? enumeration
            : default;
    }

    public static TEnum? FromName(String name) {
        return Enumerations.Values.SingleOrDefault(e => e.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }

    public static TEnum FromEnumeration(Enumeration<TEnum> enumeration) {
        return Enumerations[enumeration.Value];
    }

    public Boolean Equals(Enumeration<TEnum>? other) {
        return other is not null
            && GetType() == other.GetType()
            && this.Value == other.Value;
    }

    public sealed override Boolean Equals(Object? obj) {
        return obj is Enumeration<TEnum> other &&
               Equals(other);
    }

    public sealed override Int32 GetHashCode() {
        return this.Value.GetHashCode();
    }

    public sealed override String ToString() {
        return this.Name;
    }

    public Boolean Equals(Enumeration<TEnum>? x, Enumeration<TEnum>? y) {
        if(x is null && y is null)
            return true;
        return x is not null && y is not null && x.Equals(y);
    }

    public Int32 GetHashCode(Enumeration<TEnum> obj) {
        return obj is null ? 0 : obj.GetHashCode();
    }

    private static Dictionary<Int32, TEnum> CreateEnumerations() {
        Type enumerationType = typeof(TEnum);

        IEnumerable<TEnum> fieldsForType = enumerationType
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);

        return fieldsForType.ToDictionary(x => x.Value);
    }
}