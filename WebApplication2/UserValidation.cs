using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApplication2
{
    public class UserValidation
    {
        public static bool IsValidEmail(string emailaddress)
        {
            if (Regex.IsMatch(emailaddress, @"^([a-zA-Z0-9]+[a-zA-Z0-9\.]*[a-zA-Z0-9]+)@(gmail)\.(com)$"))
            {
                return true;
            }
            Console.WriteLine("Invalid value!");
            return false;
        }
        public static bool IsValidPassword(string value)
        {
            if (Regex.IsMatch(value, @"(?=.*[a-z])(?=.*[A-Z])") && value.Length >= 8)
            {
                return true;
            }
            Console.WriteLine("Invalid value!");
            return false;
        }
    }
}
