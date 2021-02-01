using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace EventManager
{
    public class EventsHandler
    {
        Manager manager = new Manager();
        public void AddEvent()
        {
            string EventNameInput;
            int AmountTicketsInput;
            float PricePerTicketInput;
            string DateTimeInput;
            string TempVar;

            string filename = "events.json";

            Console.WriteLine("Please enter the Event Name:");
            EventNameInput = Console.ReadLine();
            Console.WriteLine("Please enter the number of tickets available:");
            TempVar = Console.ReadLine();
            while (!int.TryParse(TempVar, out AmountTicketsInput))
            {
                Console.WriteLine("Invalid input!");
                Console.WriteLine("Please enter the number of tickets available:");
                TempVar = Console.ReadLine();
            }
            Console.WriteLine("Please enter the Price Per Ticket:");
            TempVar = Console.ReadLine();
            while (!float.TryParse(TempVar, out PricePerTicketInput))
            {
                Console.WriteLine("Invalid input!");
                Console.WriteLine("Please enter the Price Per Ticket:");
                TempVar = Console.ReadLine();
            }
            Regex DateReg = new Regex(@"([0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}[ ][0-2][0-9][:][0-9]{2})");
            Console.WriteLine("Please enter the DateTime of the event in the format DD/MM/YYYY HH:MM");
            TempVar = Console.ReadLine();
            while (DateReg.Matches(TempVar).Count == 0)
            {
                Console.WriteLine("Invalid input!");
                Console.WriteLine("Please enter the Price Per Ticket:");
                TempVar = Console.ReadLine();
            }
            DateTimeInput = TempVar;

            List<EventDetails> EventsList = new List<EventDetails>();
            string jsonData = "";

            //Read all text of file if file is not present create it first
            if (File.Exists(filename))
            {
                jsonData = System.IO.File.ReadAllText(filename);
            }
            else
            {
                var createFile = File.Create(filename);
                createFile.Close();
            }

            // De-serialize to object or create new list
            EventsList = JsonConvert.DeserializeObject<List<EventDetails>>(jsonData)
                                  ?? new List<EventDetails>();
            int LastInsertID = EventsList.Count;
            EventsList.Add(new EventDetails()
            {
                EventID = LastInsertID + 1,
                EventName = EventNameInput,
                AmountTickets = AmountTicketsInput,
                PricePerTicket = PricePerTicketInput,
                DateTime = DateTimeInput
            });

            //Put entire list back into file
            string json = JsonConvert.SerializeObject(EventsList.ToArray(), Formatting.Indented);
            System.IO.File.WriteAllText(filename, json);

        }
        public bool UpdateEvent(string username)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("Would you like to enter an Event ID or Event Name?");
            string type = Console.ReadLine();

            switch (type.ToLower())
            {
                case "id":
                    List<string> ids = new List<string>();
                    Console.WriteLine("Please enter an event id: ");
                    string ID = Console.ReadLine();

                    //foreach ([EVENT ID] idtype in [JSON FILE]) {
                    //ids.Add(idtype)
                    //}

                    if (!ids.Contains(ID))
                    {
                        Console.WriteLine("Invalid EventID entered, please try again.");
                        UpdateEvent(username);
                        return true;
                    }
                    //ID IS VALID
                    Console.Clear();
                    Console.WriteLine("\nCurrent Event Information:");
                    Console.WriteLine("Event ID: " + ID);
                    Console.WriteLine("Tickets available: ");//+ availabletickets
                    Console.WriteLine("Price per ticket: ");//+ priceperticket
                    Console.WriteLine("Date and time");//+ datetime
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nWhich part of the event would you like to update?");
                    string etype = Console.ReadLine();

                    switch (etype.ToLower())
                    {
                        case "id":
                            Console.WriteLine("\nThe current Event ID is: " + ID);
                            Console.WriteLine("Please enter a new event ID for this event.");
                            int newid = Convert.ToInt32(Console.ReadLine());

                            //update ID in JSON file

                            manager.MenuChooser(username);
                            return true;

                        case "tickets available":
                            Console.WriteLine("\nThe current tickets available are: ");//+availabletickets
                            Console.WriteLine("Please enter a new amount of tickets available for this event.");
                            int newtickets = Convert.ToInt32(Console.ReadLine());

                            //update tickets in JSON file

                            manager.MenuChooser(username);
                            return true;

                        case "price per ticket":
                            Console.WriteLine("\nThe current price per ticket is: ");//+priceperticket
                            Console.WriteLine("Please enter a new price per ticket for this event.");
                            int priceperticket = Convert.ToInt32(Console.ReadLine());

                            //update pricepertickets in JSON file

                            manager.MenuChooser(username);
                            return true;

                        case "date and time":
                            Regex DateReg = new Regex(@"([0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}[ ][0-2][0-9][:][0-9]{2})");
                            Console.WriteLine("Please enter the DateTime of the event in the format DD/MM/YYYY HH:MM");
                            string TempVar = Console.ReadLine();
                            while (DateReg.Matches(TempVar).Count == 0)
                            {
                                Console.WriteLine("Invalid input!");
                                Console.WriteLine("Please enter the DateTime of the event in the format DD/MM/YYYY HH:MM");
                                TempVar = Console.ReadLine();
                            }
                            string DateTimeInput = TempVar;
                            return true;

                        default:
                            //gonna make a function to easily display info for an event, which saves looping loads.
                            return true;

                    }//end of inner switch statement (etype)

                case "name":
                    Console.WriteLine("Please enter an event name: ");
                    string name = Console.ReadLine();
                    return true;

                default:
                    UpdateEvent(username);
                    return true;
            }

            //update event
        }
        public void DeleteEvent()
        {
            //delete event
        }
        public void BookTickets()
        {
            //book tickets
        }
        public void ListEvents()
        {
            //list events
        }
        public bool DisplayLog(string username)
        {
            string path = @"log.txt";

            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path))
                {
                    fs.Close();
                }

                Console.Write("The log file is currently empty.");
                manager.AdminMenu(username);
                return true;
            }
            Console.Clear();
            Console.WriteLine("Attempting to display log file:");
            Console.ForegroundColor = ConsoleColor.White;

            int lines = 0;
            foreach (string line in File.ReadLines(path))
            {
                lines++;
                Console.WriteLine(line);
            }


            if (lines <= 0)
            {
                Console.WriteLine("\nThe log file is empty.");
                string input = Console.ReadLine();

                if (input != null)
                    manager.MenuChooser(username);
                return true;
            }


            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nDisplayed entire log file (" + lines + " lines)");
            string input2 = Console.ReadLine();

            if (input2 != null)
                manager.MenuChooser(username);
            return true;
        }
    }
}
