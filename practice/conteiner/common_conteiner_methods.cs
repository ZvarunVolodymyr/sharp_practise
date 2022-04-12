using System.Collections;
using helping;
using validation;

namespace practice;

using CertificateClass;
using config;
public partial class conteiner<type> where type: IGetSet, new()
{
    class iterator : IEnumerator
    {
        private node? current;
        
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

        public node()
        {
            
        }
        public node(type? value, node? next = null, node? previous = null)
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

    public void insert(int key, type value)
    {
        if (key > Length)
            throw new KeyNotFoundException();
        
        if (key == 0)
        {
            AddFront(value);
            return;
        }
        if (key == Length)
        {
            Add(value);
            return;
        }

        var val = start;
        for (int i = 0; i < key; i++)
            val = val.next;
        val.next = new node(previous: val, next: val.next, value: value);
        val.next.next.previous = val.next;
    }
    public void RemoveBack()
    {
        if (end == start)
        {
            end = start = null;
            return;
        }

        end.previous.next = null;
        end = end.previous;
    }

    public void RemoveFirst()
    {
        if (end == start)
        {
            end = start = null;
            return;
        }

        start = start.next;
    }

    public void Remove(int key)
    {
        if (key >= Length)
            throw new KeyNotFoundException();
        if (key == 0)
        {
            RemoveFirst();    
            return;
        }

        if (key == Length - 1)
        {
            RemoveBack();
            return;
        }

        var val = start;
        for (int i = 0; i < key; i++)
            val = val.next;
        val.previous.next = val.next;
        val.next.previous = val.previous;
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
            
            if (key == this.Length - 1)
                end.value = value;
            
            for (int i = 0; i < key; i++)
                current = current.next;
            current.value = value;
        }
    }

    public IEnumerator GetEnumerator()
    {
        return (IEnumerator)new iterator(start);
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