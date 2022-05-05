using System;
using System.Collections.Generic;
using System.Text;


namespace TaxFree.Users
{
    abstract class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public abstract Role Role { get; }
        public string Password { get; set; }

        public virtual bool isValid()
        {
            if (ValidationForUsers.isValidFirstLastName(FirstName) &&
                ValidationForUsers.isValidFirstLastName(LastName) && ValidationForUsers.IsValidEmail(Email) &&
                ValidationForUsers.IsValidPassword(Password))
            {
                return true;
            }
            return false;
        }
    }
}
