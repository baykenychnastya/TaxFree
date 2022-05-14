using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace TaxFree.Users
{
    class ValidationForUsers
    {
        public static bool isValidFirstLastName(string value)
        {
            if(Regex.IsMatch(value, @"^[a-zA-Z]+$") && value.Length >= 2)
            {
                return true;
            }
            Console.WriteLine("Invalid value!");
            return false;
        }

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

        public static int validateIntSalary()
        {
            while (true)
            {
                int value;
                try
                {
                    value = int.Parse(Console.ReadLine()); ;
                }
                catch (Exception h)
                {
                    Console.WriteLine("Incorect value");
                    continue;
                }
                return value;
            }
        }

        public static DateTime validateFirstDayInCompany()
        {
            while (true)
            {
                DateTime value;
                try
                {
                    value = DateTime.Parse(Console.ReadLine()); ;
                }
                catch (Exception h)
                {
                    Console.WriteLine("Incorect value");
                    continue;
                }
                return value;
            }
        }

        
    }
    
}
