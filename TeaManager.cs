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
            SaveTeas(teas);
        }

        //Edit existing tea and update to file.
        public void EditTea(Tea tea, string fieldName, string fieldValue)
        {
            List<Tea> teas = ReadTeas();
            tea = teas.Find(e => e.TeaName == tea.TeaName);
            if(fieldName == "Tea Name")
            {
                tea.TeaName = fieldValue;
            }
            else if(fieldName == "Tea Type")
            {
                tea.TeaType = fieldValue;
            }
            else if(fieldName == "Company Name")
            {
                tea.CompanyName = fieldValue;
            }
            else if(fieldName == "Caffeine")
            {
                tea.ContainsCaffeine = fieldValue;
            }
            else if(fieldName == "Rating")
            {
                tea.Rating = Int32.Parse(fieldValue);
            }
            else if(fieldName == "Notes")
            {
                tea.Notes = fieldValue;
            }
            SaveTeas(teas);
        }

        //Delete an existing tea and update file.
        public void DeleteTea(string teaName)
        {
            List<Tea> teas = ReadTeas();
            Tea teaToRemove = teas.Find(t => t.TeaName == teaName);
            teas.Remove(teaToRemove);
            SaveTeas(teas);
        }

        //Serialize tea list to tea.json.
        private void SaveTeas(List<Tea> teas)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter writer = new StreamWriter(FilePath))
            using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, teas);
            }
        }
    }
}
