using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaxFree
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = readfile();
            var colection = new GenericCollection<TaxFree>(file);


            while (true)
            {
                Console.WriteLine("Enter:\n1 -  if you want to add note to the file\n" +
                    "2 - if you want to sort\n" +
                    "3 - if you want to search\n" +
                    "4 - if you want to delete the note by ID\n" +
                    "5 - if you want to edit note by ID\n" +
                    "6 - if you want to see the entire contents of the file\n" +
                    "7 - if you want to exit\n");
                int option = 0;
                try
                {
                    option = Int32.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please, enter only the options offered in the menu!");
                }
                switch (option)
                {
                    case 1:
                        colection.addNew();
                        colection.updateFile(file);
                        break;
                    case 2:
                        var sortParam = colection.validateSortParametr();
                        colection.Sort(sortParam);
                        break;
                    case 3:
                        Console.WriteLine("Enter serch string: ");
                        var serchStering = Console.ReadLine();
                        colection.Search(serchStering);
                        break;
                    case 4:
                        Console.WriteLine("Enter id: ");
                        var id = validation.validationId();
                        colection.deleteById(id);
                        colection.updateFile(file);
                        break;
                    case 5:
                        Console.WriteLine("Enter id: ");
                        var idForEdit = validation.validationId();
                        colection.editOnId(idForEdit);
                        colection.updateFile(file);
                        break;
                    case 6:
                        Console.WriteLine("See the entire contents of the file");
                        colection.printAllText(file);
                        break;
                    case 7:
                        Console.WriteLine("exit");
                        break;
                    default:
                        break;
                }
            }
        }

        static public string readfile()
        {
            var fileName = validation.validateFile();
            while (true)
            {
                if (File.Exists(fileName))
                {
                    return fileName;
                }
                else
                {
                    //using var fs = File.Create(fileName);
                    File.WriteAllText(fileName, "[]");
                }
            }
        }
    }
}
