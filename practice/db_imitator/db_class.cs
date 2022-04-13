using System.Net.Sockets;
using System.Reflection;
using CertificateClass;

namespace db_imitator;

using conteiner;
using account;
public class db_class
{
    private Dictionary<string, List<object?>> models = new Dictionary<string, List<object?>>();
    
    public List<T> get_data<T>()
    {
        return get_list<T>().Cast<T>().ToList();
    }

    public db_class()
    {
    }

    public void add<T>(T obj) where T: IGetSet
    {
        string name = typeof(T).Name;
        
        if (new query<T>(get_list<T>()).filter_by("id", obj.id).first() != null)
            models[name].Remove(obj);
        
        models[name].Add(obj);
    }

    public void remove<T>(T obj) where T : IGetSet
    {
        var name = typeof(T).Name;
        var list_ = get_data<T>();
        var to_delete = new List<T>();
        foreach (var val in list_)
            if(val.id == obj.id)
                to_delete.Add(val);

        foreach (var val in to_delete)
            models[name].Remove(val);
    }

    private List<object?> get_list<T>()
    {
        string name = typeof(T).Name;
        if (!models.ContainsKey(name))
            models[name] = new List<object?>();
        return models[name];
    }

    public query<T> get_query<T>() where T: IGetSet
    {
        return new query<T>(get_list<T>());
    }
}