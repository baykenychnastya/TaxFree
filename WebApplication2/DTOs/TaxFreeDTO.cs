using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.DTOs
{
    public class TaxFreeDTO : IValidatableObject
    {
        
        public Guid id;
        public string Company { get; set; }
        public string Country { get; set; }
        public int VatRate { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public string VatCode { get; set; }       
        public DateTime DateTaxFreeRegistration { get; set; }
        public Guid CreatedBy { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(!validation.validateRequiredStringWithTrim(Company))
            {
                yield return new ValidationResult("Country Must be at least one letter");
            }

            if (!validation.validateCountry(Country))
            {
                yield return new ValidationResult("Country must be only Italy, France, Germany! ");
            }

            if (!validation.validateVatCode(VatCode))
            {
                yield return new ValidationResult("vatCode should be in mind(VA * **_ * *_ * **)");
            }

            if (!validation.validateVatRate(VatRate))
            {
                yield return new ValidationResult("VatRate must be in range (1-40)");
            }

            if (!validation.validateLoverDate(DateOfPurchase, DateTaxFreeRegistration))
            {
                yield return new ValidationResult("dateTaxFreeRegistration nust be more than dateOfPurchase");
            }

            if (!validation.validateVatRate(VatRate))
            {
                yield return new ValidationResult("VatRate must be in range 1-41 ");
            }
        }

       
    }
}
