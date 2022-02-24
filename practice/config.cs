namespace config;
using System;
public static class config
{
    public static readonly DateTime birth_date = new DateTime(1930);
    public static readonly DateTime pandemic_start_date = new DateTime(1930);
    public static readonly DateTime certificate_end_date = new DateTime(DateTime.Now.Year + 1);

    public static readonly string[] vaccine_types = new string[]{"astrazeneca", "pfizer", "coronavac"};

    public static readonly string international_passport = "[A-Za-z]{2}[0-9]{6}"; // AA123456
    
}