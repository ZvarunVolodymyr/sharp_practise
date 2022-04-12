using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using CertificateClass;
using practice;
using helping;
using validation;
using practice;

namespace Main
{
// VaccinationRequest
// certificate_class
    using conteiner_type = certificate_class;
    public class MainMenu
    {
        public static void search(conteiner<conteiner_type> conteiner)
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
        public static void sort(conteiner<conteiner_type> conteiner)
        {
            Console.WriteLine("Write field, you want sort to do");
            conteiner.sort(Console.ReadLine());
            Console.WriteLine("Sorted");
            conteiner.write_to_file();
        }   
        
        public static void add(conteiner<conteiner_type> conteiner)
        {
            conteiner.Add(new conteiner_type().read_from_console());
            Console.WriteLine(conteiner[conteiner.Length - 1]);
            conteiner.write_to_file();
        }
        
        public static void read_from_file(conteiner<conteiner_type> conteiner)
        {
            Console.WriteLine("Write file name");
            conteiner.read_from_file(Console.ReadLine());
            Console.WriteLine(conteiner);
            conteiner.write_to_file();
        }
        
        public static void change(conteiner<conteiner_type> conteiner)
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

        public static void remove(conteiner<conteiner_type> conteiner)
        {
            Console.WriteLine("Write key to delete");
            int key = (int)validation.validation.positive_integer(int.Parse(Console.ReadLine()));
            conteiner.Remove(key);
            conteiner.write_to_file();
        }
        public static void print(conteiner<conteiner_type> conteiner)
        {
            Console.WriteLine(conteiner.ToString());
        }

        public static void clear(conteiner<conteiner_type> conteiner)
        {
            conteiner.clear();
            conteiner.write_to_file();
            Console.WriteLine("Cleared");
        }

        public static void remove_by_id(conteiner<conteiner_type> conteiner)
        {
            Console.WriteLine("write object id");
            int id = (int)validation.validation.positive_integer(int.Parse(Console.ReadLine()));
            conteiner.Remove(conteiner.get_index_by_id(id));
            conteiner.write_to_file();

            Console.WriteLine($"Remove object with id {id}");
        }
        public static void exit(conteiner<conteiner_type> conteiner)
        {
            System.Environment.Exit(0);
        }
        public static void Main(string[] args)
        {
            File.WriteAllText(config.config.output_path, "");
            var functions_ = new Dictionary<string, Action<conteiner<conteiner_type>>>()
            {
                {"search", search},
                {"sort", sort}, 
                {"read from file", read_from_file},
                {"add", add},
                {"change", change},
                {"exit", exit},
                {"print", print},
                {"remove", remove},
                {"remove by id", remove_by_id}
            };
            string[] try_until_success_list =
            {
                "search", "sort", "add", "change", "remove"
            };
            var cont = new conteiner<conteiner_type>();
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
                    Console.WriteLine($"incorrect command, {e}");
                }
            }
        }
    }
}