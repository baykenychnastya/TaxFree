using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace TaxFree.Users
{
    class UserCollection<T> where T : User
    {
        protected List<T> users = new List<T>();

        public User LogIn()
        {
           
            Console.WriteLine("Enter Email Address: ");
            var email = Console.ReadLine();
            Console.WriteLine("Enter Password: ");
            var password = Console.ReadLine();
            var user = users.FirstOrDefault(u => u.Email == email && SecurePasswordHasher.Verify(password, u.Password));              
            return user;
            
        }
       
    }
}
