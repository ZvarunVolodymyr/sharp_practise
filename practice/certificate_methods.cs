using System.Net;

namespace CertificateClass;
using System;
using validation;
public partial class certificate_class
{
    private struct read_console : helping.Interfaces.ICallAble
    {
        private certificate_class obj;
        private string name;
        public read_console(certificate_class this_, string name)
        {
            this.obj = this_;
            this.name = name;
        }
        public void call()
        {
            obj.set_field(name, Console.ReadLine());
        }
    }
    public certificate_class read_from_console()
    {
        foreach (var name in this.var_list)
        {
            Console.Write($"Write {name} field\n");
            validation_functions.try_until_success(new read_console(this, name));
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
                string[] info = text[line].Split(':');
                var name = info[0].Trim();
                var value = info[1].Trim();
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
        foreach (var name in var_list)
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
        foreach (var name in var_list)
        {
            string val = this.get_field(name).ToString();
            ans += $"{name}: {val}\n";
        }

        ans += "}\n";
        return ans;
    }

    public void set_field<T>(string name, T value)
    {
        var property = typeof(certificate_class).GetProperty(name);
        property.SetValue(this, Convert.ChangeType(value, property.GetType()));
    }
    
    public object? get_field(string name)
    {
        return typeof(certificate_class).GetProperty(name).GetValue(this);
    }
}