using System;
using System.Collections.Generic;
using System.IO;

namespace TeaLog
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = "";
            //Handle input
            while(result != "Quit")
            {
                //Provide options and ask for user input
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("To show a list of teas type Show.");
                Console.WriteLine("To edit a tea, type Edit.");
                Console.WriteLine("To exit this application type Quit.");
                result = Console.ReadLine();
                string filePath = GetFileName();
                //If they type show
                if (result == "Show")
                {
                    TeaManager teaManager = new TeaManager(filePath);
                    List<Tea> teaList = teaManager.ReadTeas();
                    foreach (var tea in teaList)
                    {
                        Console.WriteLine(tea.TeaName);
                    }
                    Console.WriteLine("Would you like to see more details about one of these teas? " +
                        "If so, type Yes. If not, type No.");
                    string answer = Console.ReadLine();
                    if(answer == "Yes")
                    {
                        Console.WriteLine("Type the name of the tea whose details you would like to view.");
                        string teaAnswer = Console.ReadLine();
                        var selectedTea = teaList.Find(s => s.TeaName == teaAnswer);
                        Console.WriteLine("\nTea Name: " + selectedTea.TeaName + "\nTea Type: " + 
                            selectedTea.TeaType + "\nCompany name:  " + selectedTea.CompanyName +
                            "\nCaffeinated: " + selectedTea.ContainsCaffeine + "\nRating(1-5, 5 being best): " + selectedTea.Rating +
                            "\nNotes: " + selectedTea.Notes + "\n");
                    }
                    else if(answer == "No")
                    {
                        continue;
                    }

                }
                //If they type edit


                //After handling input, keep asking for input until they type 'quit'
                else if (result == "Quit")
                {
                    return;
                }
                //If they type show
            }


        }

        public static string GetFileName()
        {
            string myDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(myDirectory);
            string fullFileName = Path.Combine(directory.FullName, "tea.json");
            return fullFileName;
        }
    }
}
