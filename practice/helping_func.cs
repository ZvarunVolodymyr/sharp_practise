namespace helping;

public class helping_func
{
    static public T max_or_not_null<T>(T first, T second) where T: IComparable
    {
        if (first == null || second.CompareTo(first) > 0)
            return second;
        return first;
    }
    static public T min_or_not_null<T>(T first, T second) where T: IComparable
    {
        if (first == null || second.CompareTo(first) < 0)
            return second;
        return first;
    }
}

public class Interfaces
{
    public interface ICallAble
    {
        void call();
    }
}