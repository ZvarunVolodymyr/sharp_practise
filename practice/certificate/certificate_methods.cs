using System.Net;
using System.Text.Json;
using practice.helping;

namespace CertificateClass;
using System;
using validation;

public partial class certificate_class
{
    private void read(string name)
    {
        this.set_field(name, Console.ReadLine());
    }
    public certificate_class read_from_console(string[]? exclude = null)
    {
        if (exclude == null)
            exclude = new string[0];
        foreach (var name in this.field_list)
        {
            if(exclude.Contains(name))
                continue;
            Console.Write($"Write {name.Replace('_', ' ')} field\n");
            validation_functions.try_until_success(this.read, name);
        }
        return this;
    }
    
    public override string ToString()
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web){WriteIndented = true};
        options.Converters.Add(new custom_serializer.DateOnlySerializer());
        return JsonSerializer.Serialize(this, options);
    }
}