using System.Net;

namespace CertificateClass;
using System;
using validation;

public partial class certificate_class
{
    private void read(string name)
    {
        this.set_field(name, Console.ReadLine());
    }
    public certificate_class read_from_console()
    {
        foreach (var name in this.field_list)
        {
            Console.Write($"Write {name.Replace('_', ' ')} field\n");
            validation_functions.try_until_success(this.read, name);
        }
        return this;
    }

    public List<string> parse_from_string(string[] text, ref int line)
    {
        var error = new List<string>();
        for (; line < text.Length; line++)
        {
            if (text[line].Trim()[0] == '}')
                break;
            try
            {
                var seperator_pos = text[line].IndexOf(':');
                var name = text[line].Substring(0, seperator_pos).Split('"')[1];
                var value = text[line].Substring(seperator_pos + 1).Split('"')[1];
                
                this.set_field(name, value);
            }
            catch (Exception e)
            {
                error.Add(e.Message);
            }
        }

        var missing_data = this.get_missing_data();
        if (missing_data.Length > 0)
        {
            var message = "Missing data: ";
            foreach (var missed in this.get_missing_data())
                message += missed + ", ";
            error.Add(message.Substring(0, message.Length - 2));
        }

        return error;
    }

    public bool have_value(string value_to_search)
    {
        foreach (var name in field_list)
        {
            string val = this.get_field(name).ToString();
            if (val.Contains(value_to_search))
                return true;
        }

        return false;
    }

    public override string ToString()
    {
        string ans = "{\n";
        ans += helping.helping_func.seperate<string, string[]>(this.field_list, name => 
            $"\t\"{name}\": \"{this.get_field(name)}\"", ",\n");
        ans += "\n}";
        return ans;
    }
}