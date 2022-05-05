using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using TaxFree.Users;

namespace TaxFree
{
    class GenericCollection<T> where T : IGeneralization, new()
    {
        List<T> taxFrees = new List<T>();
        public User CurrentUser { get; set; }


        public GenericCollection(string filename)
        {
            using (StreamReader r = new StreamReader(filename))
            {
                string json = r.ReadToEnd();
                try
                {
                    var taxFreesJson = JsonConvert.DeserializeObject<List<T>>(json);
                    if (taxFreesJson == null)
                    {
                        taxFrees = new List<T>();
                    }
                    for (int i = 0; i < taxFreesJson.Count; i++)
                    {
                        if (taxFreesJson[i].isValid())
                        {
                            taxFrees.Add(taxFreesJson[i]);
                        }
                        else
                        {
                            Console.WriteLine($"Element {i} is invalid");
                        }
                        //taxFrees.Add(taxFreesJson[i]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Incorect json format {ex.Message}");
                }
            }
            updateFile(filename);
        }
        public void updateFile(string filename)
        {
            var json = JsonConvert.SerializeObject(taxFrees, Formatting.Indented);
            File.WriteAllText(filename, json);
        }

        public void display(List<T> list)
        {
            foreach (T elem in list)
            {
                Console.WriteLine(elem.ToString());
            }
        }
        public void addNew()
        {
            T tax = new T();
            tax.initNew(CurrentUser.Id);
            taxFrees.Add(tax);
        }
        public void Sort(string field)
        {
            var taxFrees2 = taxFrees.OrderBy(e => e.GetType().GetProperty(field).GetValue(e, null)).ToList();
            display(taxFrees2);
        }
        public string validateSortParametr()
        {
            while (true)
            {
                var sortParam = " ";
                Console.WriteLine("Enter sort paramtr: ");
                sortParam = Console.ReadLine();
                T tax = new T();

                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(tax))
                {
                    if (sortParam == prop.Name)
                    {
                        return sortParam;
                    }
                    continue;
                }
            }
        }
        public List<T> Search(string subStr)
        {
            bool SearchPredicater(T e)
            {
                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(e))
                    if (prop.GetValue(e).ToString().Contains(subStr)) return true;
                return false;
            }
            var filtered = taxFrees.FindAll(SearchPredicater);

            display(filtered);
            return filtered;
        }

        public void deleteById(Guid id)
        {
            for (int i = 0; i < taxFrees.Count(); i++)
            {
                if (taxFrees[i].Id == id)
                {
                    taxFrees.Remove(taxFrees[i]);
                }
            }
        }

        public void editOnId(Guid id)
        {
            for (int i = 0; i < taxFrees.Count(); i++)
            {
                if (taxFrees[i].Id == id && allowToEdit(taxFrees[i]))
                {
                    taxFrees[i].Update();
                    return;
                }
            }
            Console.WriteLine("TaxFree no found");
        }
            
       
        public void printAllText()
        {
            foreach (T tax in taxFrees)
            {
                Console.WriteLine(tax.ToString());
            }
        }
         
        public void printAll()
        {
            foreach (T tax in taxFrees.Where(i=> i.CreatedBy == CurrentUser.Id))
            {
                Console.WriteLine(tax.ToString());
            }
        }

        public void printApproved()
        {
            foreach (T tax in taxFrees.Where(i => i.Status == Status.Approved))
            {
                Console.WriteLine(tax.ToString());
            }
        }

        private bool allowToEdit(T tax)
        {
            return tax.Status == Status.Draft && CurrentUser.Role == Role.Staff;          
        }

        public void filteredOnStatus(Status status)
        {
            foreach (T tax in taxFrees.Where(i => i.CreatedBy == CurrentUser.Id && i.Status == status))
            {              
                Console.WriteLine(tax.ToString());               
            }
        }

        public void approveTaxFree(Guid id)
        {
            var taxFree = taxFrees.FirstOrDefault(t => t.Status == Status.Draft && t.Id == id);
            if(taxFree == null)
            {
                Console.WriteLine("Not found...");
                return;
            }
            while (true)
            {
                Console.WriteLine("1 - approved\n" +
                "2 - rejected");
                int optionAdmin = 0;
                try
                {
                    optionAdmin = Int32.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please, enter only the options offered in the menu!");
                    continue;
                }
                switch (optionAdmin)
                {
                    case 1:
                        taxFree.Status = Status.Approved;
                        break;
                    case 2:
                        taxFree.Status = Status.Rejected;
                        break;
                }
                break;
            }
            Console.WriteLine("Enter comment: ");
            taxFree.Comment = Console.ReadLine(); 
        }

        public void moderation(Guid id)
        {
            var taxFree = taxFrees.FirstOrDefault(t => t.Status == Status.Rejected && t.Id == id);
            if (taxFree == null)
            {
                Console.WriteLine("Not found...");
                return;
            }
            taxFree.Update();
            taxFree.Status = Status.Draft;

        }
    }
}
