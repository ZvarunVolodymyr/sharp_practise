using account;
using db_imitator;
using validation;

namespace menu;

public class admin_functions
{
    public static void get_certificate()
    {
        var admin_user = (admin)session.check_creditional("admin");

        Console.WriteLine("Write down status");
        var status = Console.ReadLine();
        admin_user.print_certificates(status);
    }

    public static void change_status()
    {
        var admin_user = (admin)session.check_creditional("admin");
        var id = int.Parse( validation_functions.read_until_success("id", (obj) =>
        {
            validation.validation.positive_integer(int.Parse(obj.ToString()));
        }).ToString());
        
        var status_name = (string)validation_functions.read_until_success("status", (obj) =>
        {
            validation.validation.in_array(obj, new string[]{"approved", "rejected"});
        });
        bool status = status_name == "approved";

        Console.WriteLine("Print message in one line");
        string message = Console.ReadLine();
        
        admin_user.change_status(id, status, message);
        Console.WriteLine("STATUS CHANGED");
    }

    public static void get_staff_users()
    {
        var user_list = session.user_query.filter_by("role", "staff").all();
        foreach (var user in user_list)
        {
            Console.WriteLine(user);
            Console.WriteLine("----------------------------------------------------------------------------------");
        }
    }
    
    public static void change_salary()
    {
        var admin_user = (admin)session.check_creditional("admin");
        var id = int.Parse((string) validation_functions.read_until_success("id", (obj) =>
        {
            validation.validation.positive_integer(Convert.ToInt32(obj));
        }));
        var salary = int.Parse((string)validation_functions.read_until_success("salary", (obj) =>
        {
            validation.validation.positive_integer(Convert.ToInt32(obj));
        }));
        
        admin_user.change_salary(id, salary);
        Console.WriteLine("SALARY CHANGED");
    }
    
}