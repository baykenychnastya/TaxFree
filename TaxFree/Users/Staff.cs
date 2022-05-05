using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TaxFree.Users
{
    class Staff : User
    {
        public override Role Role => Role.Staff;

        public int Salary { get; set; }
        public DateTime FirstDayInCompany { get; set; }

        public void Input()
        {

            while (true)
            {
                Console.WriteLine("Enter FirstName: ");
                this.FirstName = Console.ReadLine();
                if (ValidationForUsers.isValidFirstLastName(this.FirstName))
                {
                    break;
                }
                continue;
            }

            while (true)
            {
                Console.WriteLine("Enter LastName: ");
                this.LastName = Console.ReadLine();
                if (ValidationForUsers.isValidFirstLastName(this.LastName))
                {
                    break;
                }
                continue;
            }

            while (true)
            {
                Console.WriteLine("Enter Email: ");
                this.Email = Console.ReadLine();
                if (ValidationForUsers.IsValidEmail(this.Email))
                {
                    break;
                }
                continue;
            }


            while (true)
            {
                Console.WriteLine("Enter Password: ");
                this.Password = Console.ReadLine();
                if (ValidationForUsers.IsValidPassword(this.Password))
                {
                    break;
                }
                continue;
            }
          
            Console.WriteLine("Enter Salary: ");
            this.Salary = ValidationForUsers.validateIntSalary();


            Console.WriteLine("Enter FirstDayInCompany: ");
            this.FirstDayInCompany = ValidationForUsers.validateFirstDayInCompany();

            this.Id = Guid.NewGuid();
            this.Password = SecurePasswordHasher.Hash(this.Password);
        }

        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public override string ToString()
        {
            string res = "";
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this))
                res += ($"{prop.Name}: {prop.GetValue(this)}\n");
            Console.WriteLine("\n");
            return res.Substring(0, res.Length - 1);
        }
    }

}
