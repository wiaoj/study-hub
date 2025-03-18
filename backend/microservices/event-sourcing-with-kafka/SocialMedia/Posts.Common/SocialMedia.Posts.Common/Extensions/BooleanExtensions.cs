using System.Diagnostics.CodeAnalysis;

namespace SocialMedia.Posts.Common.Extensions;
public static class BooleanExtensions {
    public static Boolean IsFalse([DoesNotReturnIf(false)] this Boolean value) {
        return !value;
    }

    public static Boolean IsNull<T>(this T @object) {
        return @object is null;
    }

    public static Boolean IsZero<T>(this ICollection<T> collection) {
        return collection.Count is 0;
    }
}