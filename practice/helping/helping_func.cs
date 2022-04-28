using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using account;
using practice.helping;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

    public static List<T> ListClone<T>(List<T> source)
    {
        var ans = new List<T>();
        foreach (var value in source)
            ans.Add(value);
        return ans;
    }
    public static string getHash(string text)  
    {  
        using(var sha256 = SHA256.Create())  
        {  
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));  
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();  
        }  
    }  

}
