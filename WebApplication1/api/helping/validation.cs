using System.Collections;

namespace validation;
using System;
using helping;
using System.Text.RegularExpressions;
public class validation
{
    
    static public int? positive_integer(int? value, string message="{0} should be more that zero")
    {
        if (value == null || value < 0)
            throw new Exception(String.Format(message, value));
        return value;
    }

    static public string? name(string? value, string message = "{0} isn't aplha")
    {
        var regex = "^[\\p{L}]+$";
        var split_value = value.ToString().Split('_');
        foreach (var name in split_value)
            if (!Regex.IsMatch(name, regex) || name.Length < 2)
                throw new Exception(String.Format(message, name));
        return value;
    }

    static public string? regex_match(string? value, string regex, string message="{0} wrong format")
    {
        value = value.ToString();
        if (!Regex.IsMatch(value, regex))
            throw new Exception(String.Format(message, value));
        return value;
    }

    static public T in_array<T, Iter>(T value, Iter array, string message="{0} isn't in array {1}") 
        where Iter: IEnumerable<T>
    {
        var str = "";
        foreach (var val in array)
        {
            if (Equals(val, value))
                goto good;
            if (str == "")
                str = $"{val}";
            else
                str = $"{str}, {val}";
            
        }
        throw new Exception(String.Format(message, value, str));
        good:
        return value;
    }

    static public DateTime? date_in_range(DateTime? value, DateTime? start, DateTime? end, 
        string message="{0} isn't in range {1}, {2}")
    {
        if (start > end)
            (start, end) = (end, start);
        if(value == null || start != null && value < start || end != null && value > end)
            throw new Exception(String.Format(message, value, start, end));
        return value;
    }
}