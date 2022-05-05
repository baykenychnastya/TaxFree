using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TaxFree.Users
{
    class StaffCollection : UserCollection<Staff>
    {
        public StaffCollection(string filename)
        {
            using (StreamReader r = new StreamReader(filename))
            {
                string json = r.ReadToEnd();
                try
                {
                    var staffJson = JsonConvert.DeserializeObject<List<Staff>>(json);
                    if (staffJson == null)
                    {
                        users = new List<Staff>();
                    }
                    for (int i = 0; i < staffJson.Count; i++)
                    {
                        users.Add(staffJson[i]);
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
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(filename, json);
        }

        public User addNew()
        {
            Staff st = new Staff();
            st.Input();
            users.Add(st);
            return st;
        }

        public void printAllText(string filename)
        {
            foreach (Staff staff in users)
            {
                Console.WriteLine(staff.ToString());
            }
        }

        


    }
}
