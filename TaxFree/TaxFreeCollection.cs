using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace np1
{
    class TaxFreeCollection
    {
        List<TaxFree> taxFrees = new List<TaxFree>();


        public TaxFreeCollection(string filename)
        {
            using (StreamReader r = new StreamReader(filename))
            {
                string json = r.ReadToEnd();
                try
                {
                    var taxFreesJson = JsonConvert.DeserializeObject<List<TaxFree>>(json);
                    if (taxFreesJson == null)
                    {
                        taxFrees = new List<TaxFree>();
                    }
                    for (int i = 0; i < taxFreesJson.Count; i++)
                    {
                        if (taxFreesJson[i].isValid())
                        {
                            taxFrees.Add(taxFreesJson[i]);
                        }
                        else
                        {
                            taxFrees.Remove(taxFreesJson[i]);
                            Console.WriteLine($"Element {i} is invalid");
                            updateFile(filename);

                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Incorect json format {ex.Message}");
                }
            }
        }
        public void updateFile(string filename)
        {
            var json = JsonConvert.SerializeObject(taxFrees, Formatting.Indented);
            File.WriteAllText(filename, json);
        }

        public void display(List<TaxFree> list)
        {
            foreach (TaxFree elem in list)
            {
                Console.WriteLine(elem.ToString());
            }
        }
        public void addNew()
        {
            TaxFree tax = new TaxFree();
            tax.initNew();
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
                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(Type.GetType("np1.TaxFree")))
                {
                    if (sortParam == prop.Name)
                    {
                        return sortParam;
                    }
                    continue;
                }
                //Console.WriteLine("Invalid parametr! Try again ");
            }
        }
        public List<TaxFree> Search(string subStr)
        {
            bool SearchPredicater(TaxFree e)
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
                if (taxFrees[i].Id == id)
                {
                    taxFrees[i].Update();
                    break;
                }
            }
        }
        public void printAllText(string filename)
        {
            foreach (TaxFree tax in taxFrees)
            {
                Console.WriteLine(tax.ToString());
            }
        }
    }   
}
