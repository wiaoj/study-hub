namespace EventBus.Extensions;
public static class GenericTypeExtensions {
    public static String GetGenericTypeName(this Type type) {
        String typeName;

        if(type.IsGenericType) {
            String genericTypes = String.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
            typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
        }
        else
            typeName = type.Name;

        return typeName;
    }

    public static String GetGenericTypeName(this Object @object) {
        return @object.GetType().GetGenericTypeName();
    }
}