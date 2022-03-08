using System.Collections;
using helping;
using validation;

namespace practice;

using CertificateClass;
using config;
public class conteiner<type> where type: certificate_class, new()
{
    class iterator : IEnumerator
    {
        private node current;

        public iterator(node start)
        {
            current = new node();
            current.next = start;
        }
        public bool MoveNext()
        {
            current = current.next;
            return (current != null);
        }
        public void Reset()
        {
            current = current.previous;
        }
        public object Current
        {
            get { return current.value;}
        }
    }
    public string log_file;
    private class node
    {
        public node? next;
        public node? previous;
        public type? value;

        public node(type? value=null, node? next = null, node? previous = null)
        {
            this.value = value;
            this.next = next;
            this.previous = previous;
        }
    }

    private node? start;
    public int Length;
    private node? end;
    public void Add(type value)
    {
        this.Length += 1;
        if (start == null)
        {
            start = end = new node(value);
            return;
        }

        this.end.next = new node(value, previous: this.end);
        this.end = this.end.next;
    }

    public void AddFront(type value)
    {
        this.Length += 1;
        if (start == null)
        {
            start = end = new node(value);
            return;
        }

        this.start.previous = new node(value, this.start);
        this.start = this.start.previous;
    }

    private void clear(node? vertex)
    {
        if(vertex == null)
            return;
        clear(vertex.next);
        vertex.next = null;
        vertex.previous = null;
    }
    public void clear()
    {
        clear(start);
        start = end = null;
    }

    public type? this[int key]
    {
        get
        {
            if (key >= this.Length)
                throw new IndexOutOfRangeException();
            if (key == this.Length - 1)
                return end.value;
            
            var current = start;
            for (int i = 0; i < key; i++)
                current = current.next;
            return current.value;
        }
        set
        {
            if (key >= this.Length)
                throw new IndexOutOfRangeException();
            var current = start;
            for (int i = 0; i <= key; i++)
                current = current.next;
            current.value = value;
        }
    }

    public IEnumerator GetEnumerator()
    {
        return (IEnumerator)new iterator(start);
    }

    public void read_from_file(string file_path, string log_file = "")
    {
        this.clear();
        var text = File.ReadAllLines(file_path);
        for (int line = 0; line < text.Length; line++)
        {
            if (text[line].Trim()[0] == '{')
            {
                var new_value = new type();
                line++;
                var errors = new_value.parse_from_string(text, ref line);
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
            if(this[i].get_missing_data().Length == 0 && this[i].have_value(value))
                ans.Add(this[i].id);
        }

        return ans.ToArray();
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

    public override string ToString()
    {
        var str = "";
        foreach (var value in this)
        {
            if (str == "")
                str = $"{value}";
            else
                str = $"{str},\n{value}";
        }
        return str;
    }

    public void write_to_file(string? file_path=null)
    {
        if (file_path == null)
            file_path = config.output_path;
        File.WriteAllText(file_path, this.ToString());
    }
}