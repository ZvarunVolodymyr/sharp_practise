using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace CertificateClass;
using config;
using validation;
using helping;
public partial class certificate_class
{
    
    
    public certificate_class()
    {
        this.create_fields();
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
            values["id"] = validation.positive_integer(value);
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

    public DateTime? birth_date
    {
        get => (DateTime?)values["birth_date"];
        set
        {
            
            var end = helping_func.max_or_not_null(this.start_date, DateTime.Today);
            values["birth_date"] = validation.date_in_range(value, config.birth_date, end);
        }
    }

    public DateTime? start_date
    {
        get => (DateTime?)values["start_date"];
        set
        {
            var start = helping_func.max_or_not_null(config.pandemic_start_date, this.birth_date);
            var end = helping_func.max_or_not_null(this.end_date, DateTime.Today);
            values["start_date"] = validation.date_in_range(value, start, end);
        }
    }

    public DateTime? end_date
    {
        get => (DateTime?)values["end_date"];
        set
        {
            var start = helping_func.max_or_not_null(config.pandemic_start_date, this.start_date);
            values["end_date"] = validation.date_in_range(value, start, config.certificate_end_date);
        }
    }

    
}
