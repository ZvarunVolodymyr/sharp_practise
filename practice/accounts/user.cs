using CertificateClass;
using db_imitator;
using helping;

namespace account;
using validation;

abstract public partial class user: IGetSet
{
    private string? private_first_name;
    private string? private_last_name;
    private string? private_email;
    private string? private_role;
    private string? private_password;

    virtual public string role
    {
        get => "None";
        set { }
    }
    
    public int? id
    {
        get;
        set;
    }
    public string? first_name
    {
        get => validation.exeption_if_null(private_first_name, "first_name");
        set
        {
            private_first_name = validation.name(value);
        }
    }
    public string? last_name
    {
        get => validation.exeption_if_null(private_last_name, "last_name");
        set
        {
            private_last_name = validation.name(value);
        }
    }

    public string? email
    {
        get => validation.exeption_if_null(private_email, "email");
        set
        {
            if (session.user_query.filter_by("email", value).first() != null)
                throw new Exception("user with this email already exist");
            private_email = validation.regex_match(value, "^[a-z0-9.]+@[a-z0-9.]+.[a-z0-9.]+");
        }
    }

    public string? password
    {
        get => validation.exeption_if_null(private_password, "password");
        set
        {
            private_password = validation.regex_match(value, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
        }
    }

    public void complete_registration()
    {
        validation_functions.read_until_success("first name", (val) => this.first_name = val.ToString());
        validation_functions.read_until_success("last name", (val) => this.last_name = val.ToString());
    }

    protected virtual string[] to_string_list_field()
    {
        return this.get_fields_list();
    }
    public override string ToString()
    {
        string s = "";
        foreach (var key in to_string_list_field())
        {
            s += $"{key}: {get_field(key)}";
        }

        return s;
    }
}