using db;
using Microsoft.AspNetCore.Identity;
using WebApplication1.helping;

namespace WebApplication1.models;


public class User : IdentityUser
{}

public class login
{
    [attributes.Email]
    public string? email { get; set; }
    // [attributes.Password]
    public string? password { get; set; }
}
public class register
{
    [attributes.Email]
    public string? email { get; set; }
    // [attributes.Name]
    public string? username { get; set; }
    // [attributes.Password]
    public string? password { get; set; }
}


