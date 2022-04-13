using account;
using db_imitator;

namespace CertificateClass;
using db_imitator;
public partial class certificate_class
{
    private int private_user_id;
    private string private_status = "draft";
    private string private_message = "";

    private DateTime private_updated_at = DateTime.Now;
    private DateTime private_rejected_at = DateTime.MinValue;
    
    public int user_id
    {
        get => private_user_id;
        set
        {
            session.check_creditional("staff");
            if (session.user_query.filter_by("id", value).all().Count < 0)
                throw new Exception("there is no user with this id");
            private_user_id = value;
        }
    }

    public string status
    {
        get => private_status;
        set
        {
            if (value == "draft")
            {
                session.check_creditional("staff");
                if (this.updated_at < this.rejected_at)
                    throw new Exception("you can't send for review unchanged certificate");
            }
            else
                session.check_creditional("admin");
            
            private_status = validation.validation.in_array(value, config.config.status_list);
        }
    }

    public string message
    {
        get => private_message;
        set
        {
            session.check_creditional("admin");
            private_message = value;
        }
    }

    public DateTime updated_at
    {
        get => private_updated_at;
        set
        {
            session.check_creditional("staff");
            private_updated_at = value;
        }
    }

    public DateTime rejected_at
    {
        get => rejected_at;
        set
        {
            session.check_creditional("admin");
            rejected_at = value;
        }
    }
}