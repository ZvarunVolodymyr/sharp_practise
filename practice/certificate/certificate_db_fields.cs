using account;
using db_imitator;
using helping;
using validation;

namespace CertificateClass;
using db_imitator;
public partial class certificate_class
{
    
    public int? user_id
    {
        get => validation.validation.exeption_if_null((int?)values["user_id"]);
        set
        {
            session.check_creditional("staff");
            if (session.user_query.get((int)value) == null)
                throw new Exception("there is no user with this id");
            values["user_id"] = value;
        }
    }

    public string? status
    {
        get => (string?)values["status"] ?? "draft";
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

            values["status"] = validation.validation.in_array(value, config.config.status_list);
        }
    }

    public string? message
    {
        get => (string?)values["message"] ?? "";
        set
        {
            session.check_creditional("admin");
            values["message"] = value;
        }
    }

    public DateTime? updated_at
    {
        get
        {
            if (values["updated_at"] == null)
                values["updated_at"] = DateTime.Now;
            return (DateTime?) values["updated_at"];
        }
        set
        {
            session.check_creditional("staff");
            values["updated_at"] = value;
        }
    }

    public DateTime rejected_at
    {
        get => (DateTime?) values["rejected_at"] ?? DateTime.MinValue;
        set
        {
            session.check_creditional("admin");
            values["rejected_at"] = value;
        }
    }
}