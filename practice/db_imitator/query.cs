using System.Data;
using CertificateClass;
using conteiner;
using helping;
using validation;

namespace db_imitator;

public class query<T> where T: IGetSet
{
    private List<T> original_list;
    private List<T> query_list;
    private Dictionary<int, int> ids = new Dictionary<int, int>();
    
    public query(List<T> list)
    {
        original_list = list;
        foreach (var val in list)
            query_list.Add(val);
        for (int i = 0; i < list.Count; i++)
            ids[i] = i;
    }

    public query(List<object?> list_)
    {
        var list = list_.Cast<T>().ToList();
        original_list = list;
        query_list = new List<T>();
        foreach (var val in list)
            query_list.Add(val);
            // query_list = helping_func.DeepClone(list);
        for (int i = 0; i < list.Count; i++)
            ids[i] = i;
    }

    public query<T> filter_by(string field, object? value)
    {
        return filter(obj => obj.get_field(field).Equals(value));
    }

    public query<T> filter(Func<T, bool> check)
    {
        var new_query_list = new List<T>();
        for (int i = 0; i < query_list.Count; i++)
        {
            var val = query_list[i];
            if (check(val))
            {
                int id = ids[i];
                ids.Remove(i);
                ids[new_query_list.Count] = id;
                new_query_list.Add(val);
            }
        }

        query_list = new_query_list;
        return this;
    }
    public List<T> all()
    {
        return query_list;
    }

    public void update(string field, object? value)
    {
        foreach (var i in ids.Values)
            validation_functions.print_error(obj => original_list[i].set_field(field, obj), value);
    }

    public T? first()
    {
        if (query_list.Count == 0)
            return default(T?);
        return query_list[0];
    }

}