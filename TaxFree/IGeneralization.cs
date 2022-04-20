using System;
using System.Collections.Generic;
using System.Text;

namespace TaxFree
{
    interface IGeneralization
    {
        void initNew();
        Guid Id { get; set; }
        void Update();

        bool isValid();
    }
}
