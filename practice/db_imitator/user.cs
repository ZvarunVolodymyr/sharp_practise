using System.Runtime.InteropServices;
using account;
using helping;

namespace db_imitator;

public partial class session
{
    static private user private_user;

    
    static public void login(string email, string password)
    {
        var user = user_query.filter_by("email", email).first();
        password = helping_func.getHash(password);
        
        if (user == null || user.password != password)
            throw new Exception("inncorrect email or password");
        Console.WriteLine(user.role);
        private_user = user;
    }

    static public void logout()
    {
        if (session.user == null)
            throw new Exception("you isn't log in");
        private_user = null;
    }
    static public void register(string email, string password)
    {
        var new_user = new staff() {email = email, password = password};
        var list = user_query.all();
        if (list.Count == 0)
            new_user.id = 1;
        else
            new_user.id = list[list.Count - 1].id + 1;
        
        new_user.complete_registration();
        db.add<user>(new_user);
    }
    public static user check_creditional(string role)
    {
        if (user == null || user.role != role)
            throw new Exception("creditional error");
        return user;
    }
}