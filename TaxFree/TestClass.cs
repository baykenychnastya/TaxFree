//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Reflection;
//using System.ComponentModel;
//using TaxFree;

//namespace TaxFree
//{
//    class PaymentRequest  : IGeneralization
//    {
//        private Guid id;
//        private string payer_email;
//        private double amount;
//        private string currency;
//        private DateTime payment_request_date;
//        private DateTime payment_due_to_date;
//        private string transaction_id;

//        public PaymentRequest(Guid id, string payer_email, double amount, string currency, DateTime payment_request_date, DateTime payment_due_to_date, string transaction_id)
//        {
//            this.id = id;
//            this.payer_email = payer_email;
//            this.amount = amount;
//            this.currency = currency;
//            this.payment_request_date = payment_request_date;
//            this.payment_due_to_date = payment_due_to_date;
//            this.transaction_id = transaction_id;
//        }

//        public PaymentRequest()
//        {
//        }

//        public Guid Id
//        {
//            get
//            {
//                return this.id;
//            }
//            set
//            {
//                this.id = value;    //Validation.ValidateId2(Convert.ToString(value));
//            }
//        }
//        public string PayerEmail
//        {
//            get
//            {
//                return this.payer_email;
//            }
//            set
//            {
//                this.payer_email = value;        //Validation.ValidateEmail2(value);
//            }
//        }
//        public double Amount
//        {
//            get
//            {
//                return this.amount;
//            }
//            set
//            {
//                this.amount = value;          //Validation.ValidateAmount2(Convert.ToString(value));
//            }
//        }
//        public string Currency
//        {
//            get
//            {
//                return this.currency;
//            }
//            set
//            {
//                this.currency = value;         //Validation.ValidateCurrency2(value);
//            }
//        }
//        public DateTime PaymemtRequestDate
//        {
//            get
//            {
//                return this.payment_request_date;
//            }
//            set
//            {
//                this.payment_request_date = value;             //Validation.ValidateFirstDate2(Convert.ToString(value));
//            }
//        }
//        public DateTime PaymemtDueToDate
//        {
//            get
//            {
//                return this.payment_due_to_date;
//            }
//            set
//            {
//                this.payment_due_to_date = value; //Validation.ValidateSecondDate2(Convert.ToString(value), this.payment_request_date);
//            }
//        }
//        public string TransactionId
//        {
//            get
//            {
//                return this.transaction_id;
//            }
//            set
//            {
//                this.transaction_id = value;          //Validation.ValidateTransactionId2(value);
//            }
//        }
//        private void Input()
//        {

//            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this))
//            {
//                if (prop.Name != "Id")
//                {
//                    while (true)
//                    {
//                        Console.WriteLine($"Enter {prop.Name}: ");

//                        try
//                        {
//                            prop.SetValue(this, Convert.ChangeType(Console.ReadLine(), prop.PropertyType));
//                        }
//                        catch
//                        {
//                            Console.WriteLine($"Invalid value. Cant convert to type {prop.PropertyType.Name}");
//                            continue;
//                        }

//                        if (prop.GetValue(this)?.Equals(GetDefault(prop.PropertyType)) == false)
//                        {
//                            break;
//                        }
//                    }
//                }
//            }
//        }

//        public void Update()
//        {
//            Input();
//        }


//        public static object GetDefault(Type type)
//        {
//            if (type.IsValueType)
//            {
//                return Activator.CreateInstance(type);
//            }
//            return null;
//        }
//        public void initNew()
//        {
//            Input();
//            this.Id = Guid.NewGuid();
//        }
//        public override string ToString()
//        {
//            return $"{this.id} {this.payer_email} {this.amount} {this.currency} {this.payment_request_date.ToShortDateString()} {this.payment_due_to_date.ToShortDateString()} {transaction_id}";
//        }
//        public bool found_for_search(string data)
//        {
//            bool res = false;
//            //PaymentRequest a = new PaymentRequest();
//            PropertyInfo[] properties = typeof(PaymentRequest).GetProperties();
//            foreach (PropertyInfo property in properties)
//            {
//                //Console.WriteLine(property.GetValue(a));
//                if (Convert.ToString(property.GetValue(this)).Contains(data))
//                {
//                    res = true;
//                }
//            }
//            return res;
//        }

//        public bool isValid()
//        {          
//            return true;
//        }


//    }
//}
