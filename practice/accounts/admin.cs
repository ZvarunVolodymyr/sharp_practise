using db_imitator;

namespace account;

public class admin: user
{
    override public string role
    {
        get => "admin";
    }

    public void print_certificates(string? status=null)
    {
        // var certificates_list = DB.certificate_query.filter_by("status", "draft").all();
        
        
        var certificate_query = session.certificate_query;

        if (status != null)
            certificate_query = certificate_query.filter_by("status", status);
        

        var certificates_list = certificate_query.all();
        foreach (var certificate in certificates_list)
        {
            Console.WriteLine(certificate);
            Console.WriteLine("----------------------------------------------------------------------------------");
        }
    }

    public void change_status(int id, bool status, string message = "")
    {
        var certificate = session.certificate_query.filter_by("id", id).first();
        if (certificate == null || certificate.status != "draft")
            throw new Exception($"You can't change status of certificate with id {id}");

        if (status)
            certificate.status = "approved";
        else
        {
            certificate.status = "rejected";
            certificate.message = message;
            certificate.rejected_at = DateTime.Now;
        }
        session.db.add(certificate);

    }
}
