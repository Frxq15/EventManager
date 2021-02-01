using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EventManager
{
    public class Manager
    {


        public bool Login()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please enter your username:");

            string username = Console.ReadLine();

            switch (username.ToLower())
            {
                case "demouser":
                    Console.WriteLine("Please enter your password:");
                    string password = Console.ReadLine();

                    while (!password.Equals("password"))
                    {
                        Console.WriteLine("Incorrect password entered.");
                        Console.WriteLine("Please re-enter your password:");
                        password = Console.ReadLine();
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Login Successful..");
                    MenuChooser(username);
                    return true;

                case "admin":
                    Console.WriteLine("Please enter your password:");
                    string password2 = Console.ReadLine();

                    while (!password2.Equals("root"))
                    {
                        Console.WriteLine("Incorrect password entered.");
                        Console.WriteLine("Please re-enter your password:");
                        password2 = Console.ReadLine();
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Login Successful..");
                    MenuChooser(username);
                    return true;

                default:
                    Console.WriteLine("Invalid username entered.");
                    Login();
                    return true;
            }

        }
        public bool MenuChooser(string username)
        {
            string[] admins = new string[3] { "admin", "administrator", "root" };

            if (admins.Contains(username))
            {
                AdminMenu(username);
                return true;
            }
            UserMenu(username);
            return true;
        }

        public bool AdminMenu(string username)
        {
            EventsHandler handler = new EventsHandler();
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("\t\t[+] Admin Main Menu [+]");
            Console.WriteLine("\tPlease select from one of the following options");
            Console.WriteLine("\t\t  [1, 2, 3, 4, 5, 6]\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t1: Create an event");
            Console.WriteLine("\t\t2: Update an event");
            Console.WriteLine("\t\t3: Delete an event");
            Console.WriteLine("\t\t4: Book tickets");
            Console.WriteLine("\t\t5: Display event list");
            Console.WriteLine("\t\t5: View self purchased tickets");
            Console.WriteLine("\t\t7: Display transaction log");
            Console.WriteLine("\nType 'logout' to logout of your account\n");

            string option = Console.ReadLine();


            switch (option)
            {
                case "1":
                    Console.WriteLine("Option 1 Selected.");
                    handler.AddEvent(username);
                    return true;

                case "2":
                    Console.WriteLine("Option 2 Selected.");
                    handler.UpdateEventInfo(username);
                    return true;

                case "3":
                    Console.WriteLine("Option 3 Selected.");
                    handler.DeleteEvent(username);
                    return true;

                case "4":
                    Console.WriteLine("Option 4 Selected.");
                    handler.BookTickets(username);
                    return true;

                case "5":
                    Console.WriteLine("Option 5 Selected.");
                    handler.PrintEvents(username);
                    return true;

                case "6":
                    Console.WriteLine("Option 6 Selected.");
                    handler.ViewOwnTickets(username);
                    return true;

                case "7":
                    Console.WriteLine("Option 7 Selected.");
                    handler.DisplayLog(username);
                    return true;

                case "logout":
                case "exit":
                    Console.Clear();
                    Login();
                    return true;

                default:
                    Console.WriteLine("Invalid option selected.");
                    System.Threading.Thread.Sleep(1000);
                    AdminMenu(username);
                    return true;
            }
        }
        public bool UserMenu(string username)
        {
            EventsHandler handler = new EventsHandler();
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("\t\t[+] User Main Menu [+]");
            Console.WriteLine("\tPlease select from one of the following options");
            Console.WriteLine("\t\t      [1, 2, 3]\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t1: Book tickets");
            Console.WriteLine("\t\t2: View self purchased tickets");
            Console.WriteLine("\t\t3: Display event list");
            Console.WriteLine("\nType 'logout' to logout of your account\n");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine("Option 1 Selected.");
                    handler.BookTickets(username);
                    return true;

                case "2":
                    Console.WriteLine("Option 2 Selected.");
                    handler.ViewOwnTickets(username);
                    return true;

                case "3":
                    Console.WriteLine("Option 3 Selected.");
                    handler.PrintEvents(username);
                    return true;

                case "logout":
                case "exit":
                    Console.Clear();
                    Login();
                    return true;

                default:
                    Console.WriteLine("Invalid option selected.");
                    System.Threading.Thread.Sleep(1000);
                    UserMenu(username);
                    return true;
            }
        }
    }
}
