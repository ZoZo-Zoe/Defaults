using System.Text.RegularExpressions;

namespace Defaults.Validator;

public partial class Validate
{
    public static bool NotNull<T>(T item) => item is not null;
    public static bool Null<T>(T item) => item is null;
    public static bool Empty(string item) => string.IsNullOrEmpty(item);
    public static bool NotEmpty(string item) => !string.IsNullOrEmpty(item);
    public static bool Empty<T>(IEnumerable<T> collection) => !collection.Any();
    public static bool NotEmpty<T>(IEnumerable<T> collection) => collection.Any();
    public static bool RegexMatch(string item, Regex pattern) => pattern.IsMatch(item);
    public static bool RegexNotMatch(string item, Regex pattern) => !pattern.IsMatch(item);
}