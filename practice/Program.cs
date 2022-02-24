using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using CertificateClass;
using practice;
using helping;
using validation;

namespace Main
{

    public class MainMenu
    {
        private class main_menu_struct : Interfaces.ICallAble
        {
            public conteiner<certificate_class> conteiner;

            public main_menu_struct(conteiner<certificate_class>? conteiner_ = null)
            {
                this.conteiner = conteiner_;
            }
            public void call() {}
        }

        private class change_: main_menu_struct
        {
            public change_(conteiner<certificate_class> val): base(val){}
            public void call()
            {
                Console.WriteLine("Write obj id");
                int? id = validation.validation.positive_integer(int.Parse(Console.ReadLine()));
                Console.WriteLine("Write field name");
                var name = Console.ReadLine();
                Console.WriteLine("Write new value");
                var value = Console.ReadLine();
                conteiner.change(id, name, value);
                Console.WriteLine("Changed");
            }
        }
        private class sort_: main_menu_struct
        {
            public sort_(conteiner<certificate_class> val): base(val){}
            public void call()
            {
                Console.WriteLine("Write field, you want sort to do");
                conteiner.sort(Console.ReadLine());
                Console.WriteLine("Sorted");
            }
        }
        private class read_from_file_: main_menu_struct
        {
            public read_from_file_(conteiner<certificate_class> val): base(val){}
            public void call()
            {
                Console.WriteLine("Write file name");
                conteiner.read_from_file(Console.ReadLine());
            }
        }
        public static void search(conteiner<certificate_class> conteiner)
        {
            Console.WriteLine("Write value to search:");
            var value = Console.ReadLine();
            var ans = conteiner.search(value);
            if (ans.Length == 0)
                Console.WriteLine($"There isn't {value} in conteiner");
            else
            {
                var ans_string = $"{value} finded in objects with following id's: ";
                foreach (var i in ans)
                    ans_string += i.ToString() + ", ";
                ans_string = ans_string.Substring(0, ans_string.Length - 2) + '\n';
                Console.WriteLine(ans_string);
            }
        }
        
        public static void sort(conteiner<certificate_class> conteiner)
        {
            validation_functions.try_until_success(new sort_(conteiner));
        }   

        public static void add(conteiner<certificate_class> conteiner)
        {
            conteiner.Add(new certificate_class().read_from_console());
        }
        
        public static void read_from_file(conteiner<certificate_class> conteiner)
        {
            validation_functions.try_until_success(new read_from_file_(conteiner));
        }
        
        public static void change(conteiner<certificate_class> conteiner)
        {
            validation_functions.try_until_success(new change_(conteiner));
        }

        public static void exit()
        {
            System.Environment.Exit(0);
        }
        public static void Main(string[] args)
        {
            var functions_ = new Dictionary<string, Action<conteiner<certificate_class>>>();
            functions_["search"] = search;
            functions_["sort"] = sort;
            functions_["read_from_file"] = read_from_file;
            functions_["add"] = add;
            var cont = new conteiner<certificate_class>();
            while (true)
            {
                Console.WriteLine("Choose what to do\n" +
                                  "search, sort, read_from_file, add");
                try
                {
                    functions_[Console.ReadLine()](cont);
                }
                catch (Exception e)
                {
                    Console.WriteLine("incorrect command");
                }
                    
                
            }
            
        }
    }
}