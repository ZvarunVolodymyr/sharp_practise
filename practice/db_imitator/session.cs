using System.ComponentModel.DataAnnotations;
using account;
using helping;

namespace db_imitator;

using CertificateClass;
public partial class session
{
    static public db_class db = new db_class();
    
    static public menu.menu private_menu_manager = new menu.menu();

    static public menu.menu menu_manager
    {
        get => private_menu_manager;
    }

    static public void start_session()
    {
        db = new db_class();
        private_menu_manager = new menu.menu();
        private_user = null;
    }
    
    static public query<user> user_query
    {
        get => db.get_query<user>();
    }

    static public query<certificate_class> certificate_query
    {
        get => db.get_query<certificate_class>();
    }
    
    static public user user
    {
        get => private_user;
    }
    
}