using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TeaLog
{
    public class TeaManager
    {
        //This class can do the adding, editing, deleting.
        //this will show encapsulation.
        private List<Tea> Teas;
        public string FilePath;

        public TeaManager(string filePath)
        {
            FilePath = filePath;
        }

        //read
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
        
        //Add
        public void AddTea(Tea tea)
        {
            List<Tea> teas = ReadTeas();
            teas.Add(tea);
        }

        //edit

        //delete
    }
}
