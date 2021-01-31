using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
                    string password = Console.ReadLine();
                    Console.WriteLine("Please enter your password:");
                    
                    while(!password.Equals("password"))
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

        public bool AdminMenu(string username) {
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
                Console.WriteLine("\t\t6: Display transaction log\n");

            string option = Console.ReadLine();


                switch (option)
                {
                    case "1":
                    Console.WriteLine("Option 1 Selected.");
                    return true;

                case "2":
                    Console.WriteLine("Option 2 Selected.");
                    return true;

                case "3":
                    Console.WriteLine("Option 3 Selected.");
                    return true;

                case "4":
                    Console.WriteLine("Option 4 Selected.");
                    return true;

                case "5":
                    Console.WriteLine("Option 5 Selected.");
                    return true;

                case "6":
                    Console.WriteLine("Option 6 Selected.");
                    return true;

                default:
                        return true;
            }
        }
        public bool UserMenu(string username)
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("\t\t[+] User Main Menu [+]");
            Console.WriteLine("\tPlease select from one of the following options");
            Console.WriteLine("\t\t  [1, 2, 3, 4, 5, 6]\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t1: Book tickets");
            Console.WriteLine("\t\t2: Display event list");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    return true;

                default:
                    return true;
            }
        }
    }
}
