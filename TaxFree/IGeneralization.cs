using System;
using System.Collections.Generic;
using System.Text;
using TaxFree.Users;

namespace TaxFree
{
    interface IGeneralization
    {
        void initNew(Guid id);
        Guid Id { get; set; }
        Guid CreatedBy { get; set; }
        Status Status { get; set; }
        string Comment { get; set; }

        void Update();

        bool isValid();
    }
}
