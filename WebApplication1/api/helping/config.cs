namespace helping;
using System;
public static class config
{

    // CLASS CONFIG
    public static readonly DateTime birth_date = DateTime.Now.AddYears(-200);
    public static readonly DateTime pandemic_start_date = new DateTime(2019, 1, 1);
    public static readonly DateTime certificate_end_date = DateTime.Now.AddYears(1);
    public static readonly string[] vaccine_types = new string[]{"astrazeneca", "pfizer", "coronavac", "moderna"};
    public static readonly string international_passport = "[A-Z]{2}[0-9]{6}"; // AA123456
    

    
}