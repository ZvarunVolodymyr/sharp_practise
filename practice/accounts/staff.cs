using CertificateClass;
using conteiner;
using db_imitator;

namespace account;

using validation;
public class staff: user
{
    private int id;
    private int? private_salary;
    private DateOnly? private_first_day_in_company = DateOnly.FromDateTime(DateTime.Today);
    private List<int> certificates_id = new List<int>();
    
    virtual public string role
    {
        get => "staff";
    }

    public query<certificate_class> certificates
    {
        get
        {
            return session.certificate_query.filter(obj => certificates_id.Contains((int)obj.id));
        }
    }
    public int? salary
    {
        get => validation.exeption_if_null(private_salary, "salary");
        set
        {
            session.check_creditional("admin");
            private_salary = validation.positive_integer(value);
        }
    }

    public DateOnly? first_day_in_company
    {
        get => private_first_day_in_company;
    }

    public void new_certificate()
    {
        var ans = new certificate_class();
        ans.read_from_console();
        while (session.certificate_query.filter_by("id", ans.id).first() != null)
        {
            Console.WriteLine($"id {ans.id} already in use");
            validation_functions.try_until_success((obj) => ans.set_field("id", obj), Console.ReadLine());
        }

        ans.user_id = this.id;
        this.certificates_id.Add((int)ans.id);
        session.db.add(ans);
    }

    public void print_certificate(string? status = null)
    {
        var certificate_query = certificates;

        if (status != null)
        {
            if (status == "approved")
                certificate_query = session.certificate_query;
            
            certificate_query = certificate_query.filter_by("status", status);
        }

        var certificates_list = certificate_query.all();
        foreach (var certificate in certificates_list)
        {
            Console.WriteLine(certificate);
            Console.WriteLine("----------------------------------------------------------------------------------");
        }
    }

    public certificate_class get_draft_or_rejected(int id)
    {
        var certificate = certificates.filter_by("id", id)
            .filter((obj => new string[]{"draft", "rejected"}.Contains(obj.status))).first();
        if (certificate == null)
            throw new Exception($"STAFF with id {this.id} can't change certificate with id {id}");
        return certificate;
    }

    public List<certificate_class> get_draft_or_rejected()
    {
        return certificates.filter((obj => new string[]{"draft", "rejected"}.Contains(obj.status))).all();
    }

    public void update_certificate(int id, Dictionary<string, object?> changes)
    {
        var certificate = get_draft_or_rejected(id);

        changes.Remove("updated_at");

        int update_count = 0;
        foreach (var key in changes.Keys)
        {
            if(validation_functions.print_error(obj => certificate.set_field(key, obj), changes[key]))
                update_count++;
        }
        if(update_count > 0)
            certificate.updated_at = DateTime.Now;
        
        session.db.add(certificate);
    }

    public void update_all_certificates(Dictionary<string, object?> changes)
    {
        changes["updated_at"] = DateTime.Now;
        var certificate = certificates
            .filter((obj => new string[]{"draft", "rejected"}.Contains(obj.status)));
        
        foreach (var key in changes.Keys)
            certificate.update(key, changes[key]);

    }

    public void send_to_review(int id)
    {
        var certificate = get_draft_or_rejected(id);
        certificate.status = "draft";
        session.db.add(certificate);
    }

    public void send_all_to_reviwe()
    {
        var certificate_list = get_draft_or_rejected();
        foreach (var certificate in certificate_list)
            if (certificate.updated_at > certificate.rejected_at)
            {
                certificate.status = "draft";
                session.db.add(certificate);
            }

    }
    public void remove(int id)
    {
        var certificate = certificates.filter_by("id", id).filter_by("status", "draft").first();
        if (certificate == null)
            throw new Exception($"STAFF with id {this.id} can't remove certificate with id {id}");
        this.certificates_id.Remove((int)certificate.id);
        session.db.remove(certificate);
    }

    public void remove_all()
    {
        var certificate_list = certificates.filter_by("id", id).filter_by("status", "draft").all();
        foreach (var val in certificate_list)
        {
            this.certificates_id.Remove((int)val.id);
            session.db.remove(val);
        }
    }
}