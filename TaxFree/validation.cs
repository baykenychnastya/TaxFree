using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TaxFree
{
    class validation
    {
        public static Guid validationId()
        {
            while (true)
            {
                Guid value;
                try
                {
                    value = Guid.Parse(Console.ReadLine()); ;
                }
                catch (Exception h)
                {
                    Console.WriteLine("Incorect id");
                    continue;
                }
                return value;
            }
        }
        public static bool checkIsPositive(int value)
        {
            if(value < 0)
            {
                Console.WriteLine("ID must be positive");
                return false;
            }
            return true;
        }

        public static bool validateRequiredStringWithTrim(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Must be at least one letter");
                return false;
            }
            return true;
        }

        public static bool validateCountry(string value)
        {
            string[] countries = { "Italy", "Germany", "France" };
            for (int i = 0; i < countries.Length; i++)
            {
                if (value == countries[i])
                {
                    return true;
                }
            }
            Console.WriteLine("Country must be only Italy, France, Germany! ");
            return false;

        }

        public static bool validateVatRate(int value)
        {
            if (value < 41 && value > 0)
            {
                return true;
            }
            Console.WriteLine("VatRate must be in range (1-40)");
            return false;
        }

        public static bool validateLoverDate(DateTime value1, DateTime value2)  // dateOfPurchase <= dateTaxFreeRegistration
        {
            if (value1 <= value2)
            {
                return true;
            }
            Console.WriteLine("dateTaxFreeRegistration nust be more than dateOfPurchase");
            return false;
        }

        public static bool validateVatCode(string value)
        {
            string pattern = @"^VA.{3,3}_.{2,2}_.{3,3}$";
            if (value != null && Regex.IsMatch(value, pattern))
            {
                return true;
            }
            Console.WriteLine("vatRate should be in mind (VA***_**_***) ");
            return false;
        }

        public static int readIntType()
        {
            while(true)
            {
                int value;
                try
                {
                    value = Convert.ToInt32(Console.ReadLine());
                }
                catch(Exception h)
                {
                    Console.WriteLine("Must be intager type!");
                    continue;
                }
                return value;
            }
        }
        public static DateTime readDateType()
        {
            while (true)
            {
                DateTime value;
                try
                {
                    value = Convert.ToDateTime(Console.ReadLine());
                }
                catch (Exception h)
                {
                    Console.WriteLine("Must be Data type!");
                    continue;
                }
                return value;
            }
        }
        public static string validateFile()
        {
            while (true)
            {
                Console.WriteLine("Enter file_name: ");
                var file = Console.ReadLine();
                string validFileExtensions = ".json";
                if (!validFileExtensions.Any(file.EndsWith))
                {
                    Console.WriteLine("Incorrect .json format. Try again");
                    continue;
                }
                return file;
            }
        }

    }
}
