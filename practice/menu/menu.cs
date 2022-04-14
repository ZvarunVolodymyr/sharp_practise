using account;
using db_imitator;
using helping;
using validation;

namespace menu;

public class menu
{
    private Dictionary<string, Action<object>> functions_;
    private string[] try_until_success_list = new string[0];

    public menu()
    {
        main_menu();
    }
    
    public void run()
    {
        functions_["exit"] = (_) =>
        {
            session.db.create_dump();
            System.Environment.Exit(0);
        };
        
        var str = helping_func.seperate<string, string[]>(functions_.Keys.ToArray(),
            name => name.Replace('_', ' '));
        
        Start:
        Console.WriteLine($"Choose what to do\n{str}");
        try
        {
            string command_name = Console.ReadLine().ToLower();
            foreach (var val in try_until_success_list)
                if (command_name == val)
                {
                    validation_functions.try_until_success(functions_[command_name], null);
                    goto next;
                }
            validation_functions.print_error(functions_[command_name], null);
            next:
            return;
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine($"incorrect command, {e}");
            goto Start;
        }
    }

    public void main_menu()
    {
        functions_ = new Dictionary<string, Action<object>>()
        {
            {"login", login_functions.login},
            {"register", login_functions.register}
        };
    }

    public void staff()
    {
        session.check_creditional("staff");
        
        functions_ = new Dictionary<string, Action<object>>()
        {
            {"logout", login_functions.logout},
            {"create certificate", (_) => {staff_functions.create_certificate();}},
            {"update certificate", (_) => {staff_functions.update_certificate();}},
            {"update all certificate", (_) => {staff_functions.update_all_certificate();}},
            {"get certificate", (_) => {staff_functions.get_certificate();}},
            {"send to review", (_) => {staff_functions.send_to_review();}},
            {"send all to review", (_) => {staff_functions.send_all_to_review();}},
            {"remove", (_) => {staff_functions.remove();}},
            {"remove all", (_) => {staff_functions.remove_all();}},
        };
        try_until_success_list = new[]
            { "" };
    }

    public void admin()
    {
        session.check_creditional("admin");
        functions_ = new Dictionary<string, Action<object>>()
        {
            {"logout", login_functions.logout},
            {"get certificate", (_) => {admin_functions.get_certificate();}},
            {"change status", (_) => {admin_functions.change_status();}},
            {"change salary", (_) => {admin_functions.change_salary();}}
        };
        try_until_success_list = new[]
            { "" };
    }
    
    
    
    
}