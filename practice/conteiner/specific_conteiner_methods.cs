using CertificateClass;
using validation;

namespace conteiner;

public partial class conteiner<type> where type: IGetSet
{
    private string[] get_missing_data(type obj)
    {
        var ans = new List<string>();
        foreach (var name in obj.get_fields_list())
        {
            if (obj.get_field(name) == null || obj.get_field(name).ToString() == "")
                ans.Add(name);
            var k = obj.get_field(name);
        }

        return ans.ToArray();
    }
    public List<string> parse_from_string(string[] text, ref int line, type? obj)
    {
        var error = new List<string>();
        for (; line < text.Length; line++)
        {
            if (text[line].Trim()[0] == '}')
                break;
            try
            {
                var seperator_pos = text[line].IndexOf(':');
                var name = text[line].Substring(0, seperator_pos).Split('"')[1];
                var value = text[line].Substring(seperator_pos + 1).Split('"')[1];
                obj.set_field(name, value);
            }
            catch (Exception e)
            {
                error.Add(e.Message);
            }
        }

        var missing_data = get_missing_data(obj);
        if (missing_data.Length > 0)
        {
            var message = "Missing data: ";
            foreach (var missed in missing_data)
                message += missed + ", ";
            error.Add(message.Substring(0, message.Length - 2));
        }

        return error;
    }
    public void read_from_file(string file_path, string log_file = "")
    {
        this.clear();
        var text = File.ReadAllLines(file_path);
        for (int line = 0; line < text.Length; line++)
        {
            if (text[line].Trim()[0] == '{')
            {
                var new_value = default(type);
                line++;
                var errors = this.parse_from_string(text, ref line, new_value);
                if (errors.Count != 0)
                {
                    validation_functions.log_error(errors, log_file);
                    continue;
                }
                this.Add(new_value);
            }
        }
    }

    public void sort(string field_name)
    {
        bool flag = true;
        while (flag)
        {
            flag = false;
            for (int i = 0; i < this.Length - 1; i++)
            {
                var val_1 = (IComparable)this[i].get_field(field_name);
                var val_2 = (IComparable)this[i + 1].get_field(field_name);
                if (val_1.GetType() == typeof(string))
                {
                    val_1 = val_1.ToString().ToLower();
                    val_2 = val_2.ToString().ToLower();
                }

                if (val_1.CompareTo(val_2) > 0)
                {
                    (this[i], this[i + 1]) = (this[i + 1], this[i]);
                    flag = true;
                }
            }
        }
    }
    
    public int?[] search(string value)
    {
        var ans = new List<int?>();
        for (int i = 0; i < this.Length; i++)
        {
            if(this.get_missing_data(this[i]).Length == 0)
                foreach (var key in this[i].get_fields_list())
                {
                    if (this[i].get_field(key).ToString().Contains(value))
                    {
                        ans.Add(this[i].id);
                        break;
                    }
                }
        }

        return ans.ToArray();
    }

    public int get_index_by_id(int id)
    {
        int i = 0;
        foreach (type val in this)
        {
            if (val.id == id)
                return i;
            i++;
        }

        throw new Exception($"there isn't object with id {id}");
    }

    public void change(int? id, string name, string value)
    {
        for (int i = 0; i < this.Length; i++)
        {
            if (this[i].id == id)
            {
                this[i].set_field(name, value);
                return;
            }
        }

        throw new Exception($"no object with id {id}");
    }
}