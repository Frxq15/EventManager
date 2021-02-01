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

            var jsonData = System.IO.File.ReadAllText(filename);
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

            string json = JsonConvert.SerializeObject(EventsList.ToArray());

            //write string to file
            System.IO.File.WriteAllText(filename, json);

        }
        public bool UpdateEvent()
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
                        Console.WriteLine("Invalid EventID entered, please try another ID or Name");
                        //will probably change this to a while loop later so they dont have to reinput name or id
                        UpdateEvent();
                        return true;
                    }
                    //ID IS VALID
                    Console.Clear();
                    Console.WriteLine("\nCurrent Event Information:");
                    Console.WriteLine("Event ID: " + ID);
                    Console.WriteLine("Tickets available: ");//+ availabletickets
                    Console.WriteLine("Price per ticket: ");//+ priceperticket
                    Console.WriteLine("Date and time");//+ datetime
                    Console.WriteLine("\nWhich part of the event would you like to update?");
                    string etype = Console.ReadLine();

                    switch (etype.ToLower())
                    {
                        case "id":
                            return true;

                        case "tickets available":
                            return true;

                        case "price per ticket":
                            return true;

                        case "date and time":
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
                    UpdateEvent();
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
        public bool DisplayEvent()
        {
            return true;
        }
    }
}
