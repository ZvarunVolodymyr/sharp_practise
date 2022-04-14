using System.Net.Sockets;
using System.Reflection;
using account;
using CertificateClass;
using helping;
using Newtonsoft.Json;
using practice.helping;

// using Formatting = System.Xml.Formatting;

namespace db_imitator;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        
        if (obj.id != null && new query<T>(get_list<T>()).filter_by("id", obj.id).first() != null)
            models[name].Remove(obj);
        
        models[name].Add(obj);
        commit();
    }

    private void add(object? obj, Type T)
    {
        get_list(T);
        string name = T.Name;
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
        commit();
    }

    private List<object?> get_list<T>()
    {
        string name = typeof(T).Name;
        if (!models.ContainsKey(name))
            models[name] = new List<object?>();
        return models[name];
    }
    private List<object?> get_list(Type T)
    {
        string name = T.Name;
        if (!models.ContainsKey(name))
            models[name] = new List<object?>();
        return models[name];
    }

    public query<T> get_query<T>() where T: IGetSet
    {
        return new query<T>(get_list<T>());
    }

    public void commit()
    {
        create_dump();
    }
    public void create_dump()
    {
        string folder_name = config.config.db_folder;
        string[] keys = models.Keys.ToArray();
        foreach (var model in keys)
        {
            string path = folder_name + $"/{model}.json";
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            options.Converters.Add(new custom_serializer.DateOnlySerializer());
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(JsonSerializer.Serialize(models[model], options));
            }
        }
    }
    
    public void load_dump(Type[] models_types)
    {
        this.models = new Dictionary<string, List<object?>>();
        
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        options.Converters.Add(new custom_serializer.DateOnlySerializer());
        foreach (var type in models_types)
        {
            string path = config.config.db_folder + $"/{type.Name}.json";

            var items = new List<Dictionary<string, object>>();
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                try
                {
                    items = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json, options);
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        
            Type cur_type = type;
                
            foreach (var data in items)
            {
                if (type == typeof(user))
                {
                    if (data["role"].ToString() == "staff")
                        cur_type = typeof(staff);
                    else
                        cur_type = typeof(admin);
                    data.Remove("role");
                }

                var new_obj =  (IGetSet)Activator.CreateInstance(cur_type);
                
                
                session.system = true;
                foreach (var key in data.Keys)
                    new_obj.set_field(key, data[key], true);
                session.system = false;
                this.add(new_obj, type);
            }
        }

    }
}