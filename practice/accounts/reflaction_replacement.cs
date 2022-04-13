namespace account;

abstract public partial class user
{
    public user() {}
    private static Dictionary<string, Action<user, object?>> setters = new Dictionary<string, Action<user, object?>>()
    {
        {"first_name", (obj, val) => obj.first_name = val.ToString()},
        {"last_name", (obj, val) => obj.last_name = val.ToString()},
        {"email", (obj, val) => obj.email = val.ToString()},
        {"password", (obj, val) => obj.password = val.ToString()}
    };

    public string[] get_fields_list()
    {
        return new[] {"first_name", "last_name", "email", "password"};
    }
    
    public void set_field<T>(string name, T value)
    {
        setters[name](this, value);
    }
    
    public object? get_field(string name)
    {
        return new Dictionary<string, object>()
        {
            {"first_name", this.first_name},
            {"last_name", this.last_name},
            {"email", this.email},
            {"role", this.role},
            {"password", this.password}
        }[name];
    }

}