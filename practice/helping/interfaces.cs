using db_imitator;

namespace helping;

public interface IGetSet
{
    public int? id
    {
        get;
        set;
    }

    public void set_field<T>(string name, T value, bool ignore_errors=false);
    
    public object? get_field(string name);

    public string[] get_fields_list();

}
