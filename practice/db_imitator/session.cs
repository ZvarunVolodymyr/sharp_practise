using System.ComponentModel.DataAnnotations;
using account;

namespace db_imitator;

using CertificateClass;
public class session
{
    static public db_class db = new db_class();

    static public query<user> user_query
    {
        get => db.get_query<user>();
    }

    static public query<certificate_class> certificate_query
    {
        get => db.get_query<certificate_class>();
    }

    static private user private_user;

    
    static public void login(string email, string password)
    {
        var user = user_query.filter_by("email", email).first();
        if (user == null || user.password != password)
            throw new Exception("inncorrect email or password");
        
        private_user = user;
    }

    static public void register(string email, string password)
    {
        var new_user = new staff() {email = email, password = password};

        db.add<user>(new_user);
    }

    static public user user
    {
        get => private_user;
    }

    public static void check_creditional(string role)
    {
        if (user.role != role)
            throw new Exception("creditional error");
    }
}