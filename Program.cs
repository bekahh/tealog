using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TeaLog
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get file path to teas.json.
            string myDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(myDirectory);
            string filePath = Path.Combine(directory.FullName, "tea.json");

            TeaManager teaManager = new TeaManager(filePath);
            List<Tea> teaList = teaManager.ReadTeas();

            int result = 0;
            while (result != 5)
            {
                //Provide options and ask for user input.
                Console.WriteLine("What would you like to do? Type one of the below numbers to choose.");
                Console.WriteLine("(1) Show a list of teas.");
                Console.WriteLine("(2) Add a new tea to the list.");
                Console.WriteLine("(3) Edit an existing tea.");
                Console.WriteLine("(4) Delete a tea.");
                Console.WriteLine("(5) Exit this application.");
                bool success = Int32.TryParse(Console.ReadLine(), out result );
                if (!success)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
                //If the user types 1, deserialize and display tea names.
                if (result == 1)
                {
                    teaList = teaManager.ReadTeas();
                    foreach (var tea in teaList)
                    {
                        Console.WriteLine(tea.TeaName);
                    }
                    Console.Write("Would you like to see more details about one of these teas? " +
                        "If so, type Yes. If not, type No. ");
                    //If the user types Yes, ask for the name of the tea they wish to view
                    //then show all fields for that tea.
                    string answer = Console.ReadLine();
                    if(StringExtensions.FirstCharToUpper(answer) == "Yes")
                    {
                        teaList = teaManager.ReadTeas();
                        Console.Write("Type the name of the tea whose details you would like to view: ");
                        string teaAnswer = Console.ReadLine();
                        if (teaList.Exists(t => t.TeaName == teaAnswer))
                        {
                            var selectedTea = teaList.Find(s => s.TeaName == teaAnswer);
                            Console.WriteLine("Tea Name: " + selectedTea.TeaName + "\nTea Type: " +
                                selectedTea.TeaType + "\nCompany name:  " + selectedTea.CompanyName +
                                "\nCaffeinated: " + selectedTea.ContainsCaffeine + "\nRating(1-5, 5 being best): " + selectedTea.Rating +
                                "\nNotes: " + selectedTea.Notes);
                        }
                        else
                        {
                            Console.WriteLine("That tea does not exist. Please try again.");
                            continue;
                        }
                    }
                    //If the user types no, go back to the main menu.
                    else if(StringExtensions.FirstCharToUpper(answer) == "No")
                    {
                        continue;
                    }
                }
                //If the user types 2, ask for each field then add the tea to the main list.
                else if(result == 2)
                {
                    string additionalTea = "";
                    do
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
                        Console.WriteLine("Your tea has been successfully added!");
                        //Allow adding an additional tea if desired.
                        Console.WriteLine("Would you like to add another tea? Type Yes or No: ");
                        additionalTea = Console.ReadLine();
                    } while (StringExtensions.FirstCharToUpper(additionalTea) == "Yes");
                }
                //If the user types 3, ask for name of tea to edit.
                //Ask for field to edit then get new field value.
                else if (result == 3)
                {
                    Tea teaToEdit = new Tea();
                    teaList = teaManager.ReadTeas();
                    Console.Write("Type the name of the tea that you would like to edit: ");
                    string editTeaName = Console.ReadLine();
                    if (teaList.Exists(t => t.TeaName == editTeaName))
                    {
                        string editOptions = "Tea Name, Tea Type, Company Name, Caffeine, Rating, or Notes";
                        teaToEdit = teaList.Find(t => t.TeaName == editTeaName);
                        Console.Write("Which field would you like to edit? Type one of the following: " +
                        "{0}. ", editOptions);
                        string editFieldName = Console.ReadLine();
                        if (editOptions.Contains(editFieldName))
                        {
                            Console.Write("Type the new value of the chosen field: ");
                            string editFieldValue = Console.ReadLine();
                            teaManager.EditTea(teaToEdit, editFieldName, editFieldValue);
                            Console.WriteLine("{0} was successfully changed to {1}!", editFieldName, editFieldValue);
                        }
                        else
                        {
                            Console.WriteLine("That field does not exist. Please try again.");
                            continue;
                        }

                    }
                    else
                    {
                        Console.WriteLine("That tea does not exist. Please try again.");
                        continue;
                    }
                }
                //If the user types 4, ask for name of tea then delete it from file.
                else if (result == 4)
                {
                    teaList = teaManager.ReadTeas();
                    Console.Write("Please type the name of the tea that you would like to delete: ");
                    string removeTea = Console.ReadLine();
                    if (teaList.Exists(t => t.TeaName == removeTea))
                    {
                        teaManager.DeleteTea(removeTea);
                        Console.WriteLine("{0} was successfully deleted!", removeTea);
                    }
                    else
                    {
                        Console.WriteLine("That tea does not exist. Please try again.");
                        continue;
                    }
                }
                //If user types 5, exit program.
                else if (result == 5)
                {
                    return;
                }
            }
        }
    }
}
