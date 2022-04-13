namespace config;
using System;
public static class config
{
    // GLOBAL CONFIG
    public static readonly string output_path = "ans.txt";
    public static readonly string log_path = "";
    
    // CLASS CONFIG
    public static readonly DateOnly birth_date = DateOnly.FromDateTime(DateTime.Now).AddYears(-200);
    public static readonly DateOnly pandemic_start_date = new DateOnly(2019, 1, 1);
    public static readonly DateOnly certificate_end_date = DateOnly.FromDateTime(DateTime.Now).AddYears(1);
    public static readonly string[] vaccine_types = new string[]{"astrazeneca", "pfizer", "coronavac"};
    public static readonly string international_passport = "[A-Z]{2}[0-9]{6}"; // AA123456
    
    // USER CONFIG
    public static readonly string[] user_role = new string[]{"admin", "staff"};
    public static readonly string[] status_list = new string[]{"draft", "approved", "rejected"};


}