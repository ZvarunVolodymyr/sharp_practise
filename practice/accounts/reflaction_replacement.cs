using helping;
using validation;

namespace account;

public partial class user
{
    public user()
    {

    }
    
    private static Dictionary<string, Action<user, object?>> setters = new Dictionary<string, Action<user, object?>>()
    {
        {"first_name", (obj, val) => obj.first_name = val.ToString()},
        {"last_name", (obj, val) => obj.last_name = val.ToString()},
        {"email", (obj, val) => obj.email = val.ToString()},
        {"password", (obj, val) => obj.password = val.ToString()}
    };

    public string[] get_fields_list()
    {
        return setters.Keys.ToArray();
    }
    
    public void set_field<T>(string name, T value, bool ignore_error=false)
    {
        try
        {
            setters[name](this, value);
        }
        catch (Exception e)
        {
            if (!ignore_error)
                throw e;
        }
        
    }
    
    public object? get_field(string name)
    {
        return new Dictionary<string, Func<object?>>()
        {
            {"first_name", () => this.first_name},
            {"last_name", () => this.last_name},
            {"email", () => this.email},
            {"role", () => this.role},
            {"password", () => this.password}
        }[name]();
    }

}

public partial class staff
{
    private static Dictionary<string, Action<staff, object?>> setters = new Dictionary<string, Action<staff, object?>>()
    {
        {"first_name", (obj, val) => obj.first_name = val.ToString()},
        {"last_name", (obj, val) => obj.last_name = val.ToString()},
        {"email", (obj, val) => obj.email = val.ToString()},
        {"password", (obj, val) => obj.password = val.ToString()},
        {"salary", (obj, val) => obj.salary = Convert.ToInt32(val)},
        {"first_day_in_company", (obj, val) => {}}, 
        {"certificates_id", (obj, val) => obj.certificates_id = ((List<object>)val).Cast<int>().ToList()}
    };

    public object? get_field(string name)
    {
        return new Dictionary<string, Func<object?>>()
        {
            {"first_name", ()=>this.first_name},
            {"last_name", ()=>this.last_name},
            {"email", ()=>this.email},
            {"role", ()=>this.role},
            {"password", ()=>this.password},
            {"salary", ()=>this.salary},
            {"first_day_in_company", ()=>this.first_day_in_company},
            {"certificates_id", ()=>this.certificates_id},
        }[name];
    }
}