using db;
using WebApplication1.helping;
using helping;

namespace WebApplication1.models;

public class Certificate: base_model
{
    [attributes.Name]
    public string? username
    {
        get;
        set;
    }

    [attributes.InternationalPassport]
    public string? international_passport
    {
        get;
        set;
    }

    [attributes.Vaccine]
    public string? vaccine_type
    {
        get;
        set;
    }
    [attributes.BirthDate]
    public DateTime? birth_date
    {
        get;
        set;
    }
    [attributes.StartDate]
    public DateTime? start_date
    {
        get;
        set;
    }
    [attributes.EndDate]
    public DateTime? end_date
    {
        get;
        set;
    }
}