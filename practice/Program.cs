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
            Console.WriteLine("Write field, you want sort to do");
            conteiner.sort(Console.ReadLine());
            Console.WriteLine("Sorted");
        }   

        public static void add(conteiner<certificate_class> conteiner)
        {
            conteiner.Add(new certificate_class().read_from_console());
            Console.WriteLine(conteiner[conteiner.Length - 1]);
            conteiner.write_to_file();
        }
        
        public static void read_from_file(conteiner<certificate_class> conteiner)
        {
            Console.WriteLine("Write file name");
            conteiner.read_from_file(Console.ReadLine());
            Console.WriteLine(conteiner);
            conteiner.write_to_file();
        }
        
        public static void change(conteiner<certificate_class> conteiner)
        {
            Console.WriteLine("Write obj id");
            int? id = validation.validation.positive_integer(int.Parse(Console.ReadLine()));
            Console.WriteLine("Write field name");
            var name = Console.ReadLine();
            Console.WriteLine("Write new value");
            var value = Console.ReadLine();
            conteiner.change(id, name, value);
            Console.WriteLine("Changed");
            conteiner.write_to_file();
        }

        public static void print(conteiner<certificate_class> conteiner)
        {
            Console.WriteLine(conteiner.ToString());
        }

        public static void exit(conteiner<certificate_class> conteiner)
        {
            System.Environment.Exit(0);
        }
        public static void Main(string[] args)
        {
            var functions_ = new Dictionary<string, Action<conteiner<certificate_class>>>()
            {
                {"search", search},
                {"sort", sort}, 
                {"read from file", read_from_file},
                {"add", add},
                {"change", change},
                {"exit", exit},
                {"print", print}
            };
            string[] try_until_success_list =
            {
                "search", "sort", "add", "change"
            };
            var cont = new conteiner<certificate_class>();
            var str = helping_func.seperate<string, string[]>(functions_.Keys.ToArray(),
                name => name.Replace('_', ' '));
            
            while (true)
            {
                Console.WriteLine($"Choose what to do\n{str}");
                try
                {
                    string command_name = Console.ReadLine().ToLower();
                    foreach (var val in try_until_success_list)
                        if (command_name == val)
                        {
                            validation_functions.try_until_success(functions_[command_name], cont);
                            goto next;
                        }
                    validation_functions.print_error(functions_[command_name], cont);
                    next:
                    continue;
                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine($"incorrect command, e");
                }
            }
        }
    }
}