namespace helping;

public class helping_func
{
    static public T max_or_not_null<T>(T first, T second) where T: IComparable
    {
        if (first == null || second != null && second.CompareTo(first) > 0)
            return second;
        return first;
    }
    static public DateTime? max_or_not_null(DateTime? first, DateTime? second)
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
}
