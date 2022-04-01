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

    public override string ToString()
    {
        string ans = "{\n";
        ans += helping.helping_func.seperate<string, string[]>(this.field_list, name => 
            $"\t\"{name}\": \"{this.get_field(name)}\"", ",\n");
        ans += "\n}";
        return ans;
    }
    
}