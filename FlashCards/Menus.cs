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

        if (message != "") Console.WriteLine($"\n{message}");

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

    private static void StacksMenu(string message = "")
    {
        Console.Clear();

        if (message != "") Console.WriteLine($"\n{message}");

        Console.WriteLine("\nSTACKS\n");
        Console.WriteLine("- Type 1 Create a new stack");
        Console.WriteLine("- Type 2 to View your stacks");
        Console.WriteLine("- Type 3 Modify your stacks"); // Comprise both CRUD Update and Delete
        Console.WriteLine("- Type 0 Return to the main menu");

        string userInput = Console.ReadLine();  // Need improvments

        switch (userInput)
        {
            case "0":
                MainMenu();
                break;
            case "1":
               // CreateStacks();
                break;
            case "2":
               // ViewStacks();
                break;
            case "3":
               // ModifyStacks();
                break;
            default:
                // DisplayError("Wrong input ! Please type a number between 0 and 2")
                StacksMenu("Wrong input ! Please type a number between 0 and 3");
                break;
        }
    }

    private static void StudySessionsMenu(string message = "")
    {
        Console.Clear();

        if (message != "") Console.WriteLine($"\n{message}");
        Console.WriteLine("\nSTUDY SESSIONS\n");
        Console.WriteLine("- Type 1 to Start a Study Session");
        Console.WriteLine("- Type 2 to View Sessions history");
        Console.WriteLine("- Type 0 Return to the main menu");

        string userInput = Console.ReadLine();  // Need improvments

        switch (userInput)
        {
            case "0":
                MainMenu();
                break;
            case "1":
                // StartStudySession();
                break;
            case "2":
                // ViewStudySesssionsHistory();
                break;
            default:
                // DisplayError("Wrong input ! Please type a number between 0 and 2")
                StacksMenu("Wrong input ! Please type a number between 0 and 3");
                break;
        }
    }
}
