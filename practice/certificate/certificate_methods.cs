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

    protected virtual string[] to_string_list_field()
    {
        string[] additional = new[]
        {
            "user_id", "status", "updated_at", "rejected_at", "message"
        };
        string[] ans = new string[additional.Length + get_fields_list().Length];
        int i = 0;
        foreach (var val in additional)
        {
            ans[i] += val;
            i++;
        }
        foreach (var val in get_fields_list())
        {
            ans[i] += val;
            i++;
        }
        return ans;
    }
    public override string ToString()
    {
        string s = "";
        foreach (var key in to_string_list_field())
        {
            s += $"{key}: {get_field(key)}; ";
        }

        return s;
    }
}