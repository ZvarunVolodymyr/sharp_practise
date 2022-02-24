using System.Text.RegularExpressions;

namespace CertificateClass;
using config;
using validation;
using helping;
public partial class certificate_class
{
    private int? id_;
    private string? username_;
    private string? international_passport_;
    private string? vaccine_type_;
    private DateTime? start_date_;
    private DateTime? end_date_;
    private DateTime? birth_date_;

    public readonly string[] var_list = new string[]
    {
        "id", "username", "international_passport", "vaccine_type", "birth_date", "start_date", "end_date"
    };

    public string[] get_missing_data()
    {
        var ans = new List<string>();
        foreach (var name in this.var_list)
        {
            if (this.get_field(name) == null)
                ans.Add(name);
        }
        return ans.ToArray();
    }
    public int? id
    {
        get => id_;
        set
        {
            id_ = validation.positive_integer(value);
        }
    }

    public string? username
    {
        get => username_;
        set
        {
            username_ = validation.name(value);
        }
    }

    public string? international_passport
    {
        get => international_passport_;
        set
        {
            international_passport_ = validation.international_passport(value);
        }
    }

    public string? vaccine_type
    {
        get => vaccine_type_;
        set
        {
            vaccine_type_ = validation.in_array(value, config.vaccine_types);
        }
    }

    public DateTime? birth_date
    {
        get => birth_date_;
        set
        {
            var end = helping_func.max_or_not_null(this.start_date_, DateTime.Today);
            birth_date_ = validation.date_in_range(value, config.birth_date, end);
        }
    }

    public DateTime? start_date
    {
        get => start_date_;
        set
        {
            var start = helping_func.max_or_not_null(config.pandemic_start_date, this.birth_date_);
            var end = helping_func.max_or_not_null(this.end_date_, DateTime.Today);
            this.start_date_ = validation.date_in_range(value, start, end);
        }
    }

    public DateTime? end_date
    {
        get => end_date_;
        set
        {
            var start = helping_func.max_or_not_null(config.pandemic_start_date, this.start_date_);
            this.end_date_ = validation.date_in_range(value, start, config.certificate_end_date);
        }
    }
}