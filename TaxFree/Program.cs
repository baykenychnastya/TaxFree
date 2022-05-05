using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TaxFree.Users;

namespace TaxFree
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Enetr filename taxFree: ");
            var fileTaxFree = readfile();
            Console.WriteLine("Enetr filename Staff: ");
            var fileStaff = readfile();
            Console.WriteLine("Enetr filename Admin: ");
            var fileAdmin = readfile();
            var colectionTaxFrees = new GenericCollection<TaxFree>(fileTaxFree);
            var staffCollection = new StaffCollection(fileStaff);
            var adminCollection = new AdminCollection(fileAdmin);
            
            menuUser(staffCollection, fileStaff, colectionTaxFrees, fileTaxFree, adminCollection, fileAdmin);

           
        }

        static public void menuUser(StaffCollection staffCollection, string fileStaff, GenericCollection<TaxFree> colectionTaxFrees, string fileTaxFree, AdminCollection adminCollection, string fileAdmin)
        {
            while (true)
            {
                Console.WriteLine("Enter:\n1 -  if you want to sign in\n" +
                    "2 - if you want to sign up\n");

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
                        var user = ChooseRole(staffCollection, colectionTaxFrees, adminCollection);
                        if (user == null)
                        {
                            Console.WriteLine("---there are no coincidences---\n");
                            continue;
                        }
                        colectionTaxFrees.CurrentUser = user;
                        if(user.Role == Role.Staff)
                        {
                            RunStaffMenu(staffCollection, fileStaff, colectionTaxFrees, fileTaxFree);
                        }
                        else
                        {
                            RunAdminMenu(staffCollection, colectionTaxFrees, fileTaxFree);          
                        }   
                        break;
                    case 2:
                        var newUser = staffCollection.addNew();
                        staffCollection.updateFile(fileStaff);
                        colectionTaxFrees.CurrentUser = newUser;
                        RunStaffMenu(staffCollection, fileStaff, colectionTaxFrees, fileTaxFree);
                        break;
                }
            }
        }


        static public void menuTaxFree(GenericCollection<TaxFree> colectionTaxFrees, string fileTaxFree)
        {
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
                        colectionTaxFrees.addNew();
                        colectionTaxFrees.updateFile(fileTaxFree);
                        break;
                    case 2:
                        var sortParam = colectionTaxFrees.validateSortParametr();
                        colectionTaxFrees.Sort(sortParam);
                        break;
                    case 3:
                        Console.WriteLine("Enter serch string: ");
                        var serchStering = Console.ReadLine();
                        colectionTaxFrees.Search(serchStering);
                        break;
                    case 4:
                        Console.WriteLine("Enter id: ");
                        var id = validation.validationId();
                        colectionTaxFrees.deleteById(id);
                        colectionTaxFrees.updateFile(fileTaxFree);
                        break;
                    case 5:
                        Console.WriteLine("Enter id: ");
                        var idForEdit = validation.validationId();
                        colectionTaxFrees.editOnId(idForEdit);
                        colectionTaxFrees.updateFile(fileTaxFree);
                        break;
                    case 6:
                        Console.WriteLine("See the entire contents of the file");
                        colectionTaxFrees.printAllText();
                        break;
                    case 7:
                        Console.WriteLine("exit");
                        break;
                    default:
                        break;
                }
            }
        }


        static private User ChooseRole(StaffCollection staffCollection, GenericCollection<TaxFree> colectionTaxFrees, AdminCollection adminCollection)
        {
            
            Console.WriteLine("Enter:\n1 -  if you want to be admin\n" +
                "2 - if you want to be staff");
            int optionRole = 0;
            try
            {
                optionRole = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine("Please, enter only the options offered in the menu!");
            }
            switch (optionRole)
            {
                case 1:
                    return adminCollection.LogIn();
                case 2:
                    return staffCollection.LogIn();
                default:
                    Console.WriteLine("Please, enter only the options offered in the menu!");
                    return ChooseRole(staffCollection, colectionTaxFrees, adminCollection);
            }           
        }

        static private void RunStaffMenu(StaffCollection staffCollection, string fileStaff, GenericCollection<TaxFree> colectionTaxFrees, string fileTaxFree)
        {
            while (true)
            {
                Console.WriteLine("Enter:\n1 -  if you want to edit note TaxFree by ID\n" +
                    "2 - if you want to add note to the file TaxFree\n" +
                    "3 - if you want to delete the note TaxFree by ID\n" +
                    "4 - if you want to see the entire contents of the file added current staff\n" +
                    "5 - if you want filter by status\n" +
                    "6 - if yo want to see all instances of the classes TaxFree which added by the Staff and all instances with status \"Approved\"\n" +
                    "7 - if you want to send an instance of the class to the admin moderation again, changing it");
                int optionStaff = 0;
                try
                {
                    optionStaff = Int32.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please, enter only the options offered in the menu!");
                }
                switch (optionStaff)
                {
                    case 1:
                        Console.WriteLine("Enter id: ");
                        var idForEdit = validation.validationId();
                        colectionTaxFrees.editOnId(idForEdit);
                        colectionTaxFrees.updateFile(fileTaxFree);
                        break;
                    case 2:
                        colectionTaxFrees.addNew();
                        colectionTaxFrees.updateFile(fileTaxFree);
                        break;
                    case 3:
                        Console.WriteLine("Enter id: ");
                        var id = validation.validationId();
                        colectionTaxFrees.deleteById(id);
                        colectionTaxFrees.updateFile(fileTaxFree);
                        break;
                    case 4:
                        Console.WriteLine("See the entire contents of the file added current staff");
                        colectionTaxFrees.printAll();
                        break;
                    case 5:
                        Console.WriteLine("Enter status: 1-Draft\n" +
                            "2 - Approved\n" +
                            "3 - Rejected");
                        var status = Console.ReadLine();
                        if(!Enum.TryParse(status, out Status s))
                        {
                            Console.WriteLine("Invalid...");
                            break;
                        }
                        colectionTaxFrees.filteredOnStatus(s);
                        break;
                    case 6:
                        Console.WriteLine("See the entire contents of the file which added by the Staff and all instances with status \"Approved\"");
                        colectionTaxFrees.printApproved();
                        break;
                    case 7:
                        Console.WriteLine("Enter id: ");
                        var id_ = validation.validationId();
                        colectionTaxFrees.moderation(id_);
                        colectionTaxFrees.updateFile(fileTaxFree);
                        break;

                }
            }
        }

        static private void RunAdminMenu(StaffCollection staffCollection, GenericCollection<TaxFree> colectionTaxFrees, string fileTaxFree)
        {
            while (true)
            {
                Console.WriteLine("Enter:\n1 -  if you want to see all object TaxFree\n" +
                    "2 - if you want to approve draft TaxFrees\n");
                int optionAdmin = 0;
                try
                {
                    optionAdmin = Int32.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please, enter only the options offered in the menu!");
                }
                switch (optionAdmin)
                {
                    case 1:
                        Console.WriteLine("See the entire contents of the file");
                        colectionTaxFrees.printAllText();
                        break;
                    case 2:
                       
                        Console.WriteLine("Enter id: ");
                        var idForApproved = validation.validationId();
                        colectionTaxFrees.approveTaxFree(idForApproved);
                        colectionTaxFrees.updateFile(fileTaxFree);

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
