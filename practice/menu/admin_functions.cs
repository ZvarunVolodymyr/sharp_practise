using account;
using db_imitator;
using validation;

namespace menu;

public class admin_functions
{
    public static void get_certificate()
    {
        var admin_user = (admin)session.check_creditional("admin");

        var status = (string)validation_functions.read_until_success("status", (obj) =>
        {
            validation.validation.in_array(obj, config.config.status_list);
        });
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
            validation.validation.in_array(obj, config.config.status_list);
        });
        bool status = status_name == "approved";

        Console.WriteLine("Print message in one line");
        string message = Console.ReadLine();
        
        admin_user.change_status(id, status, message);
    }

    public static void change_salary()
    {
        var admin_user = (admin)session.check_creditional("admin");
        var id = (int) validation_functions.read_until_success("id", (obj) =>
        {
            validation.validation.positive_integer(Convert.ToInt32(obj));
        });
        var salary = (int) validation_functions.read_until_success("salary", (obj) =>
        {
            validation.validation.positive_integer(Convert.ToInt32(obj));
        });
        
        admin_user.change_salary(id, salary);
    }
    
}