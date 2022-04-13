using System.Runtime.Serialization.Formatters.Binary;

namespace helping;
using System.IO;
public class helping_func
{
    static public T max_or_not_null<T>(T first, T second) where T: IComparable
    {
        if (first == null || second != null && second.CompareTo(first) > 0)
            return second;
        return first;
    }
    static public DateOnly? max_or_not_null(DateOnly? first, DateOnly? second)
    {
        if (first == null || second != null && second > first)
            return second;
        return first;
    }
    static public T? min_or_not_null<T>(T? first, T? second) where T: IComparable
    {
        if (first == null || second != null && second.CompareTo(first) < 0)
            return second;
        return first;
    }
    static public string seperate<T, obj>(obj list, Func<T, object?> format,string sep=", ") where obj: IEnumerable<T>
    {
        string ans = "";
        foreach (var val in list)
        {
            if (ans == "")
                ans = $"{format(val)}";
            else
                ans = $"{ans}{sep}{format(val)}";
        }

        return ans;
    }
    public static T DeepClone<T>(T obj)
    {
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;

            return (T) formatter.Deserialize(ms);
        }
    }
}
