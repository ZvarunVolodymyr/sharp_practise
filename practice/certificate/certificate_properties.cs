using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using db_imitator;

namespace CertificateClass;
using config;
using validation;
using helping;
public partial class certificate_class
{
    
    
    public certificate_class()
    {
        this.create_fields();
        session.system = true;
        this.status = "draft";
        this.rejected_at = DateTime.MinValue;
        this.updated_at = DateTime.Now;
        session.system = false;
    }
    public string[] get_missing_data()
    {
        var ans = new List<string>();
        foreach (var name in this.field_list)
            if (this.get_field(name) == null || this.get_field(name).ToString() == "")
                ans.Add(name);
        return ans.ToArray();
    }
    public int? id
    {
        get => (int?)values["id"];
        set
        {
            int? id_ = validation.positive_integer(value);
            if (session.certificate_query.get((int) value) != null)
                throw new Exception($"Certificate with {id_} already exist");
            values["id"] = id_;
        }
    }

    public string? username
    {
        get => (string?)values["username"];
        set
        {
            values["username"] = validation.name(value);
        }
    }

    public string? international_passport
    {
        get => (string?)values["international_passport"];
        set
        {
            values["international_passport"] = validation.regex_match(value.ToUpper(), config.international_passport);
        }
    }

    public string? vaccine_type
    {
        get => (string?)values["vaccine_type"];
        set
        {
            values["vaccine_type"] = validation.in_array(value.ToLower(), config.vaccine_types);
        }
    }

    public DateOnly? birth_date
    {
        get => (DateOnly?)values["birth_date"];
        set
        {
            
            var end = helping_func.max_or_not_null(this.start_date, DateOnly.FromDateTime(DateTime.Today));
            values["birth_date"] = validation.date_in_range(value, config.birth_date, end);
        }
    }

    public DateOnly? start_date
    {
        get => (DateOnly?)values["start_date"];
        set
        {
            var start = helping_func.max_or_not_null(config.pandemic_start_date, this.birth_date);
            var end = helping_func.max_or_not_null(this.end_date, DateOnly.FromDateTime(DateTime.Today));
            values["start_date"] = validation.date_in_range(value, start, end);
        }
    }

    public DateOnly? end_date
    {
        get => (DateOnly?)values["end_date"];
        set
        {
            var start = helping_func.max_or_not_null(config.pandemic_start_date, this.start_date);
            values["end_date"] = validation.date_in_range(value, start, config.certificate_end_date);
        }
    }

    
}
