using System;
using System.Collections.Generic;
using System.IO;

namespace TeaLog
{
    class Program
    {
        static void Main(string[] args)
        {
            string myDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(myDirectory);
            string filePath = Path.Combine(directory.FullName, "tea.json");

            string result = "";
            TeaManager teaManager = new TeaManager(filePath);

            while (result != "Quit")
            {
                //Provide options and ask for user input.
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("To show a list of teas type Show.");
                Console.WriteLine("To add a new tea to the list, type Add.");
                Console.WriteLine("To delete a tea, type Delete.");
                Console.WriteLine("To exit this application type Quit.");
                result = Console.ReadLine();
                //If the user type show, deserialize and display tea names.
                if (result == "Show")
                {
                    List<Tea> teaList = teaManager.ReadTeas();
                    foreach (var tea in teaList)
                    {
                        Console.WriteLine(tea.TeaName);
                    }
                    Console.Write("Would you like to see more details about one of these teas? " +
                        "If so, type Yes. If not, type No. ");
                    
                    //If the user types Yes, ask for the name of the tea they wish to view
                    //then show all fields for that tea.
                    string answer = Console.ReadLine();
                    if(answer == "Yes")
                    {
                        Console.Write("Type the name of the tea whose details you would like to view: ");
                        string teaAnswer = Console.ReadLine();
                        var selectedTea = teaList.Find(s => s.TeaName == teaAnswer);
                        Console.WriteLine("\nTea Name: " + selectedTea.TeaName + "\nTea Type: " + 
                            selectedTea.TeaType + "\nCompany name:  " + selectedTea.CompanyName +
                            "\nCaffeinated: " + selectedTea.ContainsCaffeine + "\nRating(1-5, 5 being best): " + selectedTea.Rating +
                            "\nNotes: " + selectedTea.Notes + "\n");
                    }
                    //If the user types no, go back to the main menu.
                    else if(answer == "No")
                    {
                        continue;
                    }

                }
                //If the user types Add, ask for each field then run add the tea to the main list.
                else if(result == "Add")
                {
                    Console.Write("Type the name of the tea to add: ");
                    string newTeaName = Console.ReadLine();
                    Console.Write("Enter the tea type(ex. \"green\"):  ");
                    string newTeaType = Console.ReadLine();
                    Console.Write("Type the name of the company the tea came from: ");
                    string newCompanyName = Console.ReadLine();
                    Console.Write("Does the tea contain caffeine? Type Yes or No: ");
                    string newContainsCaffeine = Console.ReadLine();
                    Console.Write("Give this tea a rating on a scale of 1 to 5 (5 being the best): ");
                    string newRating = Console.ReadLine();
                    Console.Write("Type any notes you have about the tea: ");
                    string newNotes = Console.ReadLine();
                    Tea newTea = new Tea
                    {
                        TeaName = newTeaName,
                        TeaType = newTeaType,
                        CompanyName = newCompanyName,
                        ContainsCaffeine = newContainsCaffeine,
                        Rating = Int32.Parse(newRating),
                        Notes = newNotes
                    };
                    teaManager.AddTea(newTea);
                    Console.WriteLine("\nYour tea has been successfully added!");
                }
                //If they type Delete
                else if (result == "Delete")
                {
                    Console.Write("Please type the name of the tea that you would like to delete: ");
                    string removeTea = Console.ReadLine();

                }
                //After handling input, keep asking for input until the user types 'Quit'
                else if (result == "Quit")
                {
                    return;
                }
            }
        }
    }
}
