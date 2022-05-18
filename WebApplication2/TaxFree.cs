using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2
{
    public class TaxFree
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        private Guid id;
        private string company;
        private string country;
        private int vatRate;
        private DateTime dateOfPurchase;
        private string vatCode;
        private DateTime dateTaxFreeRegistration;

        private void Input()
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this))
            {
                if (prop.Name != "Id")
                {
                    while (true)
                    {
                        Console.WriteLine($"Enter {prop.Name}: ");

                        try
                        {
                            prop.SetValue(this, Convert.ChangeType(Console.ReadLine(), prop.PropertyType));
                        }
                        catch
                        {
                            Console.WriteLine($"Invalid value. Cant convert to type {prop.PropertyType.Name}");
                            continue;
                        }

                        if (prop.GetValue(this)?.Equals(GetDefault(prop.PropertyType)) == false)
                        {
                            break;
                        }
                    }
                }
            }
        }

        public void Update()
        {
            while (true)
            {
                Console.WriteLine("Enter Company: ");
                this.company = Console.ReadLine();
                if (validation.validateRequiredStringWithTrim(this.company))
                {
                    break;
                }
                continue;
            }

            while (true)
            {
                Console.WriteLine("Enter Country: ");
                this.country = Console.ReadLine();
                if (validation.validateCountry(this.country))
                {
                    break;
                }
                continue;
            }

            while (true)
            {
                Console.WriteLine("Enter VatRate: ");
                this.vatRate = validation.readIntType();
                if (validation.validateCountry(this.country))
                {
                    break;
                }
                continue;
            }


            Console.WriteLine("Enter DateOfPurchase: ");
            this.dateOfPurchase = validation.readDateType();

            while (true)
            {
                Console.WriteLine("Enter VatCode: ");
                this.vatCode = Console.ReadLine();
                if (validation.validateVatCode(this.vatCode))
                {
                    break;
                }
                continue;
            }

            while (true)
            {
                Console.WriteLine("Enter DateTaxFreeRegistration: ");
                this.dateTaxFreeRegistration = validation.readDateType();
                if (validation.validateLoverDate(this.dateOfPurchase, this.dateTaxFreeRegistration))
                {
                    break;
                }
                continue;
            }

        }


        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public string Company
        {
            get => company;
            set
            {
                if (validation.validateRequiredStringWithTrim(value) == true)
                {
                    company = value;
                }
            }

        }
        public string Country
        {
            get => country;
            set
            {
                if (validation.validateCountry(value) == true)
                {
                    country = value;
                }
            }
        }
        public int VatRate
        {
            get => vatRate;
            set
            {
                if (validation.validateVatRate(value) == true)
                {
                    vatRate = value;
                }
            }
        }

        public DateTime DateOfPurchase
        {
            get => dateOfPurchase;
            set => dateOfPurchase = value;
        }
        public string VatCode
        {
            get => vatCode;
            set
            {
                if (validation.validateVatCode(value) == true)
                {
                    vatCode = value;
                }
            }
        }

        public DateTime DateTaxFreeRegistration
        {
            get => dateTaxFreeRegistration;
            set
            {
                if (validation.validateLoverDate(dateOfPurchase, value) == true)
                {
                    dateTaxFreeRegistration = value;
                }
            }
        }

        public TaxFree()
        {
        }

        public void initNew(Guid userId)
        {
            Input();
            this.Id = Guid.NewGuid();
            this.CreatedBy = userId;
        }

        public TaxFree(Guid ID, string company, string country, int vatRate,
            DateTime dateOfPurchase, string vatCode, DateTime dateTaxFreeRegistration)
        {
            this.id = ID;
            this.company = company;
            this.country = country;
            this.vatRate = vatRate;
            this.dateOfPurchase = dateOfPurchase;
            this.vatCode = vatCode;
            this.dateTaxFreeRegistration = dateTaxFreeRegistration;
        }

        public bool isValid()
        {
            if (validation.validateRequiredStringWithTrim(company) &&
                validation.validateCountry(country) && validation.validateVatRate(vatRate) && validation.validateVatCode(vatCode) &&
                validation.validateLoverDate(dateOfPurchase, dateTaxFreeRegistration))
            {
                return true;
            }
            return false;
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
