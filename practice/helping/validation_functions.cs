using System.Runtime.InteropServices.ComTypes;

namespace validation;
using System;


static public class validation_functions
{
    static private string formated_write<T>(T error) 
        where T: IEnumerable<string>
    {
        string ans = "----------------------------------------------------------------\n";
        foreach (var value in error)
            ans += value + '\n';
        ans += "----------------------------------------------------------------\n";
        return ans;
    }
    static public void log_error<T>(T error, string log_file = "") where T: IEnumerable<string>
    {
        var flag = false;
        if (log_file != "")
        {
            // var file = new file_pathInfo(log_file);
            var file = File.AppendText(log_file);
            file.Write(formated_write(error));
            file.Close();
            return;
        }
        Console.Write(formated_write(error));
    }


    // DON'T KNOW HOW TO WRITE THIS NOW
    
    public static void try_until_success<T>(Action<T> func, T parametr)
    {
        while (true)
        {
            try
            {
                func(parametr);
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ". Try again");
            }
        }
    }
    public static bool print_error<T>(Action<T> func, T value, string log_file = "")
    {
        try
        {
            func(value);
        return true;
        }
        catch (Exception e)
        {
            if (log_file == "")
                Console.WriteLine(e.Message);
            return false;
        }
    }
}