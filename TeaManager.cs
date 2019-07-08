using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TeaLog
{   //This class does the adding, editing, deleting of the tea.
    public class TeaManager
    {
        private List<Tea> Teas;
        public string FilePath;

        public TeaManager(string filePath)
        {
            FilePath = filePath;
        }

        //Deserialize list of teas from file and return.
        public List<Tea> ReadTeas()
        {
            List<Tea> teas = new List<Tea>();
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(FilePath))
            using (var jsonReader = new JsonTextReader(reader))
            {
                teas = serializer.Deserialize<List<Tea>>(jsonReader);
            }
            return teas;
        }
        
        //Add new tea to tea list then serialize back to original file.
        public void AddTea(Tea tea) 
        {
            List<Tea> teas = ReadTeas(); 
            teas.Add(tea);

            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter writer = new StreamWriter(FilePath))
            using(JsonTextWriter jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, teas);
            }
        }


    }
}
