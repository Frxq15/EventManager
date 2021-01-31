using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
                    MainMenu("demouser");
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
                    MainMenu("admin");
                    return true;

                default:
                    Console.WriteLine("Invalid username entered.");
                    Login();
                    return true;
            }
     
        }
        public void MainMenu(string username) {
            if(username.Equals("admin"))
            {

            }
            //USERNAME !ADMIN
        }
    }
}
