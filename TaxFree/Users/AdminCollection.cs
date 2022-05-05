using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TaxFree.Users
{
    class AdminCollection : UserCollection<Admin>
    {
        public AdminCollection(string filename)
        {
            using (StreamReader r = new StreamReader(filename))
            {
                string json = r.ReadToEnd();
                try
                {
                    var adminJson = JsonConvert.DeserializeObject<List<Admin>>(json);
                    if (adminJson == null)
                    {
                        users = new List<Admin>();
                    }
                    for (int i = 0; i < adminJson.Count; i++)
                    {
                        users.Add(adminJson[i]);
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
    }

}
