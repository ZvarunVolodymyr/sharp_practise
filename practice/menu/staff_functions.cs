using account;
using CertificateClass;
using db_imitator;
using validation;

namespace menu;

public class staff_functions
{
    public static void create_certificate()
    {
        var staff_user = (staff)session.check_creditional("staff");
        staff_user.new_certificate();
    }

    private static Dictionary<string, object?> print_changes()
    {
        var changes = new Dictionary<string, object?>();

        while (true)
        {
            Console.WriteLine("Write field or press enter to continue");
            var field = Console.ReadLine().Trim();
            if(field == "")
                break;
            Console.WriteLine("Write value");
            var value = Console.ReadLine().Trim();

            changes[field] = value;
        }

        return changes;
    }
    
    public static void update_certificate()
    {
        var staff_user = (staff)session.check_creditional("staff");
        var id = int.Parse((string) validation_functions.read_until_success("id", (obj) =>
        {
            int id_ = (int)validation.validation.positive_integer(int.Parse(obj.ToString()));
            if (session.certificate_query.get(id_) == null)
                throw new Exception("There isn't certificate with such id");
        }));

        staff_user.get_draft_or_rejected(id);
        staff_user.update_certificate(id, print_changes());
    }

    public static void update_all_certificate()
    {
        var staff_user = (staff)session.check_creditional("staff");
        staff_user.update_all_certificates(print_changes());
    }
    
    
    public static void get_certificate()
    {
        var staff_user = (staff)session.check_creditional("staff");
        
        Console.WriteLine("Write down status");
        var status = Console.ReadLine();
        staff_user.print_certificate(status);

    }

    public static void send_to_review()
    {
        var staff_user = (staff)session.check_creditional("staff");
        var id = int.Parse((string)validation_functions.read_until_success("id", (obj) =>
        {
            int id_ = (int)validation.validation.positive_integer(int.Parse(obj.ToString()));
            if (session.certificate_query.get(id_) == null)
                throw new Exception("There isn't certificate with such id");
        }));
        
        staff_user.send_to_review(id);
    }

    public static void send_all_to_review()
    {
        var staff_user = (staff)session.check_creditional("staff");
        
        staff_user.send_all_to_reviwe();
    }

    public static void remove()
    {
        var staff_user = (staff)session.check_creditional("staff");
        var id = int.Parse((string)validation_functions.read_until_success("id", (obj) =>
        {
            validation.validation.positive_integer(Convert.ToInt32(obj));
        }));
        
        staff_user.remove(id);
    }

    public static void remove_all()
    {
        var staff_user = (staff)session.check_creditional("staff");
        staff_user.remove_all();
    }
}