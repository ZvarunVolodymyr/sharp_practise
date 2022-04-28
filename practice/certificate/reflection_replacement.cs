using helping;

namespace CertificateClass;


public partial class certificate_class : IGetSet
{
    private string[] field_list; 
    private Dictionary<string, Action<object?>> setters;
    private Dictionary<string, object?> values = new Dictionary<string, object?>();

    private void create_fields()
    {
        setters = new Dictionary<string, Action<object?>>()
        {
            {"id", val => this.id = int.Parse(val.ToString())},
            {"username", val => this.username = val.ToString()},
            {"international_passport", val => this.international_passport = val.ToString()},
            {"vaccine_type", val => this.vaccine_type = val.ToString()},
            {"birth_date", val => this.birth_date = DateOnly.Parse(val.ToString())},
            {"start_date", val => this.start_date = DateOnly.Parse(val.ToString())},
            {"end_date", val => this.end_date = DateOnly.Parse(val.ToString())}, 
            {"user_id", val => this.user_id = int.Parse(val.ToString())},
            {"status", val => this.status = val.ToString()},
            {"message", val => this.message = val.ToString()},
            {"updated_at", val => this.updated_at = DateTime.Parse(val.ToString())},
            {"rejected_at", val => this.rejected_at = DateTime.Parse(val.ToString())},
        };
        var temp = setters.Keys.ToList();
        foreach (var name in temp)
            values[name] = null;
        temp.Remove("user_id");
        temp.Remove("status");
        temp.Remove("updated_at");
        temp.Remove("rejected_at");
        temp.Remove("message");
        field_list = temp.ToArray();
        
    }

    public string[] get_fields_list()
    {
        return this.field_list;
    }
    public void set_field<T>(string name, T value, bool ignore_errors=false)
    {
        try
        {
            this.setters[name](value);
        }
        catch (Exception e)
        {
            if (!ignore_errors)
                throw e;
        }
        
    }
    
    public object? get_field(string name)
    {
        return this.values[name];
    }
}