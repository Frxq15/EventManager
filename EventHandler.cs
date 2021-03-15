using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace EventManager
{
    public class EventsHandler
    {
        Manager manager = new Manager();
        public void AddEvent()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Event creation loaded.");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();

            string EventNameInput;
            int AmountTicketsInput;
            float PricePerTicketInput;
            string DateTimeInput;
            string TempVar;

            string filename = "events.json";

            Console.WriteLine("Running event creation process.");
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
            Console.WriteLine("Please enter the Date & Time of the event in the format DD/MM/YYYY HH:MM");
            TempVar = Console.ReadLine();
            while (DateReg.Matches(TempVar).Count == 0)
            {
                Console.WriteLine("Invalid input!");
                Console.WriteLine("Please enter the Date & Time of the event in the format DD/MM/YYYY HH:MM");
                TempVar = Console.ReadLine();
            }
            DateTimeInput = TempVar;

            List<EventDetails> EventsList = GetEvents();
            int LastInsertID = 0;
            if (EventsList.Count > 0)
                LastInsertID = EventsList[EventsList.Count - 1].EventID;


            EventsList.Add(new EventDetails()
            {
                EventID = LastInsertID + 1,
                EventName = EventNameInput,
                AmountTickets = AmountTicketsInput,
                PricePerTicket = PricePerTicketInput,
                DateTime = DateTimeInput
            });

            int EventID = LastInsertID + 1;

            //Put entire list back into file
            string json = JsonConvert.SerializeObject(EventsList.ToArray(), Formatting.Indented);
            System.IO.File.WriteAllText(filename, json);
            AddLog("[" + manager.getUser() + "]" + "Added EventID [" + EventID + "], EventName [" + EventNameInput + "], AmountTickets [" + AmountTicketsInput + "], PricePerTicket [" + PricePerTicketInput + "], DateTime [" + DateTimeInput + "]");

            Console.WriteLine("Event created successfully. Press any key to continue.");
            Console.ReadKey();
            manager.MenuChooser(manager.getUser());

        }
        public void UpdateEventInfo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Updating event information loaded.");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Running event information update process.");
            //TODO create function to load events from json or do it within main
            //TODO add search by name
            EventsHandler handler = new EventsHandler();

            List<EventDetails> EventsList = GetEvents();
            int LastInsertID = 0;
            if (EventsList.Count > 0)
                LastInsertID = EventsList[EventsList.Count - 1].EventID;

            Console.WriteLine("Please enter an event ID or Name:");
            EventDetails UpdateEvent;
            int UpdateEventID;
            string EventName = Console.ReadLine();
            int EventID;
            if (int.TryParse(EventName, out EventID))
            {
                UpdateEvent = EventsList.Find(EventDetails => EventDetails.EventID == EventID);
                UpdateEventID = EventsList.FindIndex(EventDetails => EventDetails.EventID.Equals(EventID));

            }
            else
            {
                UpdateEvent = EventsList.Find(EventDetails => EventDetails.EventName.Equals(EventName));
                UpdateEventID = EventsList.FindIndex(EventDetails => EventDetails.EventName.Equals(EventName));
            }
            if (UpdateEvent == null)
            {
                Console.WriteLine("The event specified does not exist.");
                handler.UpdateEventInfo();
                return;
            }


            //ID IS VALID
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Current Event Information:");
            Console.WriteLine("Event ID: " + UpdateEvent.EventID);
            Console.WriteLine("Event Name: " + UpdateEvent.EventName);
            Console.WriteLine("Tickets available: " + UpdateEvent.AmountTickets);
            Console.WriteLine("Price per ticket: " + UpdateEvent.PricePerTicket);
            Console.WriteLine("Date and time: " + UpdateEvent.DateTime);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nWhich part of the event would you like to update?");
            string etype = Console.ReadLine();


            if (etype.ToLower().Contains("name")) {
                Console.WriteLine("\nThe current Event Name is: " + UpdateEvent.EventName);
                Console.WriteLine("Please enter a new event name.");
                UpdateEvent.EventName = Console.ReadLine();
                UpdateEventList(UpdateEventID, UpdateEvent);
                AddLog("[" + manager.getUser() + "] updated [" + UpdateEvent.EventName + "]");
                Console.WriteLine("The event name has been updated. Press any key to continue.");
                Console.ReadKey();
                handler.UpdateEventInfo();
                return;
            }
            if (etype.ToLower().Contains("tickets available") || etype.ToLower().Contains("tickets") || etype.ToLower().Contains("ticket") || etype.ToLower().Contains("amount"))
            {
                Console.WriteLine("\nThe current number of tickets available are: " + UpdateEvent.AmountTickets);
                Console.WriteLine("Please enter a new amount of tickets available for this event.");
                UpdateEvent.AmountTickets = Convert.ToInt32(Console.ReadLine());

                //update tickets in JSON file
                UpdateEventList(UpdateEventID, UpdateEvent);
                AddLog("[" + manager.getUser() + "] updated [" + UpdateEvent.EventName + "]");
                Console.WriteLine("Number of tickets has been updated. Press any key to continue.");
                Console.ReadKey();
                handler.UpdateEventInfo();
            }

            if (etype.ToLower().Contains("price per ticket") || etype.ToLower().Contains("price") || etype.ToLower().Contains("cost"))
            {
                Console.WriteLine("\nThe current price per ticket is: " + UpdateEvent.PricePerTicket);
                Console.WriteLine("Please enter a new price per ticket for this event.");
                UpdateEvent.PricePerTicket = Convert.ToInt32(Console.ReadLine());

                //update pricepertickets in JSON file
                UpdateEventList(UpdateEventID, UpdateEvent);
                AddLog("[" + manager.getUser() + "] updated [" + UpdateEvent.EventName + "]");
                Console.WriteLine("Price per ticket has been updated. Press any key to continue.");
                Console.ReadKey();
                handler.UpdateEventInfo();
            }

            if (etype.ToLower().Contains("date") || etype.ToLower().Contains("time"))
            {
                Console.WriteLine("The current Date and Time of the event is: " + UpdateEvent.DateTime);
                Regex DateReg = new Regex(@"([0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}[ ][0-2][0-9][:][0-9]{2})");
                Console.WriteLine("Please enter the Date and Time of the event in the format DD/MM/YYYY HH:MM");
                string TempVar = Console.ReadLine();
                while (DateReg.Matches(TempVar).Count == 0)
                {
                    Console.WriteLine("Invalid input given.");
                    Console.WriteLine("Please enter the Date and Time of this event in the format DD/MM/YYYY HH:MM");
                    TempVar = Console.ReadLine();
                }
                UpdateEvent.DateTime = TempVar;
                UpdateEventList(UpdateEventID, UpdateEvent);
                AddLog("[" + manager.getUser() + "] updated [" + UpdateEvent.EventName + "]");
                Console.WriteLine("The number of tickets for this event has been updated. Press any key to continue.");
                Console.ReadKey();
                manager.MenuChooser(manager.getUser());
            }
            else {
                    Console.WriteLine("Invalid type specified, retrying event update.");
                System.Threading.Thread.Sleep(2000);
                UpdateEventInfo();
            }
        }

        public bool DeleteEvent()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Event deletion loaded.");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            EventsHandler handler = new EventsHandler();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Running event deletion process.");

            string filename = "events.json";
            List<EventDetails> EventsList = GetEvents();

            Console.WriteLine("Please enter the ID of event you would like to delete: ");
            int EventID = int.Parse(Console.ReadLine());
            EventDetails UpdateEvent = EventsList.Find(EventDetails => EventDetails.EventID == EventID);
            int UpdateEventID = EventsList.FindIndex(EventDetails => EventDetails.EventID == EventID);
            if (UpdateEvent == null)
            {
                Console.WriteLine("The event specified does not exist, Press any key to continue.");
                Console.ReadKey();
                handler.DeleteEvent();
                return true;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Current Event Information:");
            Console.WriteLine("Event ID: " + UpdateEvent.EventID);
            Console.WriteLine("Event Name: " + UpdateEvent.EventName);
            Console.WriteLine("Tickets available: " + UpdateEvent.AmountTickets);//+ availabletickets
            Console.WriteLine("Price per ticket: " + UpdateEvent.PricePerTicket);//+ priceperticket
            Console.WriteLine("Date and time: " + UpdateEvent.DateTime);//+ datetime
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Are you sure you want to delete " + UpdateEvent.EventName);
            if (Console.ReadLine() == "yes")
            {
                EventsList.RemoveAt(UpdateEventID);
                string json = JsonConvert.SerializeObject(EventsList.ToArray(), Formatting.Indented);
                System.IO.File.WriteAllText(filename, json);
                Console.WriteLine("Event " + UpdateEvent.EventName + " has been removed. Press any key to continue.");
                AddLog("[" + manager.getUser() + "] deleted event [" + UpdateEvent.EventName + "]");
                Console.ReadKey();
                manager.MenuChooser(manager.getUser());
            }

            return true;
        }

        public bool BookTickets()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ticket booking loaded.");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            EventsHandler handler = new EventsHandler();


            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Running ticket booking process.");
            List<EventDetails> EventsList = GetEvents();
            int LastInsertID = EventsList[EventsList.Count - 1].EventID;
            Console.WriteLine("Please enter an event ID or Name:");
            EventDetails UpdateEvent;
            int UpdateEventID;

            string EventName = Console.ReadLine();
            int EventID;
            if (int.TryParse(EventName, out EventID))
            {
                UpdateEvent = EventsList.Find(EventDetails => EventDetails.EventID == EventID);
                UpdateEventID = EventsList.FindIndex(EventDetails => EventDetails.EventID.Equals(EventID));

            }
            else
            {
                UpdateEvent = EventsList.Find(EventDetails => EventDetails.EventName.Equals(EventName));
                UpdateEventID = EventsList.FindIndex(EventDetails => EventDetails.EventName.Equals(EventName));
            }
            if (UpdateEvent == null)
            {
                Console.WriteLine("The event specified does not exist, Press any key to continue.");
                Console.ReadKey();
                handler.BookTickets();
                return true;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nCurrent Event Information:");
            Console.WriteLine("Event ID: " + UpdateEvent.EventID);
            Console.WriteLine("Event Name: " + UpdateEvent.EventName);
            Console.WriteLine("Tickets available: " + UpdateEvent.AmountTickets);
            Console.WriteLine("Price per ticket: " + UpdateEvent.PricePerTicket);
            Console.WriteLine("Date and time: " + UpdateEvent.DateTime);
            Console.ForegroundColor = ConsoleColor.Red;
            if (UpdateEvent.AmountTickets > 0)
            {
                Console.WriteLine("Please enter your name:");
                string CustomerName = Console.ReadLine();
                Console.WriteLine("Please enter your address:");
                string CustomerAddress = Console.ReadLine();
                Console.WriteLine("Please enter number of tickets:");
                int NumberTickets = int.Parse(Console.ReadLine());
                while (UpdateEvent.AmountTickets - NumberTickets < 0 || NumberTickets < 1)
                {
                    Console.WriteLine("Invalid Number of tickets!");
                    Console.WriteLine("Please enter number of tickets:");
                    NumberTickets = int.Parse(Console.ReadLine());
                }

                Console.WriteLine("Total cost: £" + UpdateEvent.PricePerTicket * NumberTickets);

                BookingDetails booking = new BookingDetails();
                booking.CustomerName = CustomerName;
                booking.CustomerAddress = CustomerAddress;
                booking.NumberTickets = NumberTickets;
                booking.PurchasedBy = manager.getUser();

                UpdateEvent.AmountTickets -= NumberTickets;
                UpdateEvent.BookingDetails.Add(booking);

                UpdateEventList(UpdateEventID, UpdateEvent);
                AddLog("[" + CustomerName + "] booked [" + NumberTickets + "] tickets for [£" + UpdateEvent.PricePerTicket * NumberTickets + "]");
                manager.MenuChooser(manager.getUser());
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There are no tickets available for this event, Press any key to continue.");
                Console.ReadKey();
                handler.BookTickets();
            }
            return true;
        }

        public bool DisplayLog()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Displaying log file loaded.");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            string path = @"log.txt";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Running log display.");

            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path))
                {
                    fs.Close();
                }

                Console.Write("The log file is currently empty.");
                manager.AdminMenu(manager.getUser());
                return true;
            }
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThe log file is empty.");
                string input = Console.ReadLine();

                if (input != null)
                    manager.MenuChooser(manager.getUser());
                return true;
            }


            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nDisplayed entire log file (" + lines + " lines)");
            Console.WriteLine("Enter a key to continue");
            Console.ReadKey();
            ConsoleKeyInfo input2 = Console.ReadKey();

            if (input2 != null)
                manager.MenuChooser(manager.getUser());
            return true;
        }
        public void AddLog(string str)
        {
            string filepath = @"log.txt";
            StreamWriter sw = File.AppendText(filepath);
            sw.WriteLine("[" + DateTime.Now + "] " + str);
            sw.Close();
        }

        public List<EventDetails> GetEvents()
        {
            string filename = "events.json";

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
            return EventsList;
        }

        public void UpdateEventList(int ListID, EventDetails UpdateEvent)
        {
            string filename = "events.json";

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

            EventsList[ListID] = UpdateEvent;

            //Put entire list back into file
            string json = JsonConvert.SerializeObject(EventsList.ToArray(), Formatting.Indented);
            System.IO.File.WriteAllText(filename, json);
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("The event was updated successfully. Press any key to continue.");
            Console.ReadKey();
        }
        public void PrintEvents()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Viewing all events loaded.");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            List<EventDetails> EventsList = GetEvents();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Running list all events.");
            foreach (EventDetails Event in EventsList)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nEvent ID: " + Event.EventID);
                Console.WriteLine("Event Name: " + Event.EventName);
                Console.WriteLine("Tickets available: " + Event.AmountTickets);//+ availabletickets
                Console.WriteLine("Price per ticket: " + Event.PricePerTicket);//+ priceperticket
                Console.WriteLine("Date and time: " + Event.DateTime + "\n");//+ datetime
                Console.WriteLine("Booking Details: ");
                foreach (BookingDetails Booking in Event.BookingDetails)
                {
                    Console.WriteLine("  Name: " + Booking.CustomerName);
                    Console.WriteLine("  Address: " + Booking.CustomerAddress);
                    Console.WriteLine("  Number of tickets: " + Booking.NumberTickets);
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            manager.MenuChooser(manager.getUser());
        }
        public void PrintUserEvents()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Viewing all events loaded.");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            List<EventDetails> EventsList = GetEvents();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Running list all events.");
            foreach (EventDetails Event in EventsList)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nEvent ID: " + Event.EventID);
                Console.WriteLine("Event Name: " + Event.EventName);
                Console.WriteLine("Tickets available: " + Event.AmountTickets);
                Console.WriteLine("Price per ticket: " + Event.PricePerTicket);
                Console.WriteLine("Date and time: " + Event.DateTime + "\n");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            manager.MenuChooser(manager.getUser());
        }

        public void ViewOwnTickets()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Viewing self purchased tickets loaded.");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Running list self purchased tickets.");
            string NameString = manager.getUser();
            List<EventDetails> EventsList = GetEvents();
            List<EventDetails> UserEventsList = new List<EventDetails>();
            int count = 0;
            foreach (EventDetails Event in EventsList)
            {
                foreach (BookingDetails Booking in Event.BookingDetails)
                {
                    if (Booking.PurchasedBy == NameString)
                    {
                        if (!UserEventsList.Any(EventDetails => EventDetails.EventID.Equals(Event.EventID)))
                        {
                            count++;
                            UserEventsList.Add(Event);
                        }

                    }
                }
                if (count <= 0)
                {
                    Console.WriteLine("You have no tickets purchased.");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    manager.MenuChooser(manager.getUser());
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            foreach (EventDetails UserEvent in UserEventsList)
            {
                Console.WriteLine("\nEvent ID: " + UserEvent.EventID);
                Console.WriteLine("Event Name: " + UserEvent.EventName);
                Console.WriteLine("Date and time: " + UserEvent.DateTime + "\n");
                Console.WriteLine("Booking Details: ");
                foreach (BookingDetails Booking in UserEvent.BookingDetails)
                {
                    if (Booking.PurchasedBy == NameString)
                    {
                        Console.WriteLine("  Name: " + Booking.CustomerName);
                        Console.WriteLine("  Address: " + Booking.CustomerAddress);
                        Console.WriteLine("  Number of tickets: " + Booking.NumberTickets);
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            manager.MenuChooser(manager.getUser());
        }
    }
}
