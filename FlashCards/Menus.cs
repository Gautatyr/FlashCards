using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards;

public static class Menus
{
    public static void MainMenu(string message = "")
    {
        Console.Clear();

        Console.WriteLine($"\n{message}");

        Console.WriteLine("\nMAIN MENU\n");
        Console.WriteLine("- Type 1 to access the Stacks menu");
        Console.WriteLine("- Type 2 to access the Study Sessions menu");
        Console.WriteLine("- Type 0 to close the application");
        
        string userInput = Console.ReadLine();  // Need improvments

        switch(userInput)
        {
            case "0":
                Environment.Exit(0);
                break;
            case "1":
                StacksMenu();
                break;
            case "2":
                StudySessionsMenu();
                break;
            default:
                // DisplayError("Wrong input ! Please type a number between 0 and 2")
                MainMenu("Wrong input ! Please type a number between 0 and 2");
                break;
        }
    }

    private static void StacksMenu()
    {

    }

    private static void StudySessionsMenu()
    {

    }
}
