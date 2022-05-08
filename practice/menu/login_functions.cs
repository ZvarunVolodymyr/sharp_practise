using account;
using db_imitator;
using validation;

namespace menu;

public class login_functions
{
    public static void login(object null_)
    {
        Console.WriteLine("Write your email");
        var email = Console.ReadLine().Trim();

        Console.WriteLine("Write your password");
        var password = Console.ReadLine().Trim();

        session.login(email, password);

        var user_role = session.user.role;
        if (user_role == "admin")
            session.menu_manager.admin();
        if (user_role == "staff")
            session.menu_manager.staff();
    }

    public static void register(object null_)
    {
        var user_to_login = new staff();

        var email = (string?)validation_functions.read_until_success("email", 
            (obj) => user_to_login.set_field("email", obj));
        
        var password = (string?)validation_functions.read_until_success("password", 
            (obj) => user_to_login.set_field("password", obj));
        
        session.register(email, password);
    }


    public static void logout(object null_)
    {
        session.logout();
        session.menu_manager.main_menu();
        Console.WriteLine("Commit changes? (y/n)");
        string flag = (string)validation_functions.read_until_success("y or n",
            (obj) => validation.validation.in_array(((string)obj).Trim().ToLower(), new []{"y", "n"}));
        if(flag == "y")
            session.db.commit();
    }
}