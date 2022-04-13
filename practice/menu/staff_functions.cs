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

            changes["fields"] = value;
        }

        return changes;
    }
    
    public static void update_certificate()
    {
        var staff_user = (staff)session.check_creditional("staff");
        var id = (int) validation_functions.read_until_success("id", (obj) =>
        {
            validation.validation.positive_integer(Convert.ToInt32(obj));
        });

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

        var status = (string)validation_functions.read_until_success("status", (obj) =>
        {
            validation.validation.in_array(obj, config.config.status_list);
        });
        staff_user.print_certificate(status);

    }

    public static void send_to_review()
    {
        var staff_user = (staff)session.check_creditional("staff");
        var id = (int) validation_functions.read_until_success("id", (obj) =>
        {
            validation.validation.positive_integer(Convert.ToInt32(obj));
        });
        
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
        var id = (int) validation_functions.read_until_success("id", (obj) =>
        {
            validation.validation.positive_integer(Convert.ToInt32(obj));
        });
        
        staff_user.remove(id);
    }

    public static void remove_all()
    {
        var staff_user = (staff)session.check_creditional("staff");
        staff_user.remove_all();
    }
}