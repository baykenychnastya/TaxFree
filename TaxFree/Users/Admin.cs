using System;
using System.Collections.Generic;
using System.Text;

namespace TaxFree.Users
{
    class Admin : User
    {
        public override Role Role => Role.Admin;
    }
}
