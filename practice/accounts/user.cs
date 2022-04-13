using CertificateClass;

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
        get;
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
            private_email = validation.regex_match(value, "^[a-z0-9.]+@[a-z0-9.]+.[a-z0-9.]+");
        }
    }

    // public string? role
    // {
    //     get => validation.exeption_if_null(private_role, "role");
    //     set
    //     {
    //         private_role = validation.in_array(value, config.config.user_role);
    //     }
    // }
    public string? password
    {
        get => validation.exeption_if_null(password, "password");
        set
        {
            password = helping.helping_func.getHash(validation.regex_match(value, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"));
        }
    }
}