using static FlashCards.Helpers;
using static FlashCards.DataValidation;
using static FlashCards.DataAccess;

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

        switch(GetNumberInput())
        {
            case 0:
                Environment.Exit(0);
                break;
            case 1:
                StacksMenu();
                break;
            case 2:
                StudySessionsMenu();
                break;
            default:
                string error = "Wrong input ! Please type a number between 0 and 2";
                MainMenu(DisplayError(error));
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
        Console.WriteLine("- Type 3 to Update stacks");
        Console.WriteLine("- Type 4 to Delete stacks");
        Console.WriteLine("- Type 0 Return to the main menu");

        switch (GetNumberInput())
        {
            case 0:
                MainMenu();
                break;
            case 1:
                CreateStack();
                break;
            case 2:
               // ViewStacks();
                break;
            case 3:
               // UpdateStacks();
                break;
            case 4:
                // DeleteStacks();
                break;
            default:
                string error = "Wrong input ! Please type a number between 0 and 3";
                StacksMenu(DisplayError(error));
                break;
        }
    }

    private static void CreateStack()
    {
        Console.WriteLine("\nSTACK CREATION\n");

        string stackTheme = GetTextInput("Type the theme of the new stack, or type 0 to go back to the stack menu.");

        if (stackTheme == "0") StacksMenu();

        InsertStack(stackTheme);

        Console.WriteLine("\nTo insert a new card in the stack type 1, else press enter to go back to the stack menu.\n");
        string continueToAddCard = Console.ReadLine();

        while(continueToAddCard == "1")
        {
            AddCard(stackTheme);
            Console.WriteLine("Type 1 to add another card, else press enter.");
            continueToAddCard = Console.ReadLine();
        }

        StacksMenu();
    }

    private static void AddCard(string stackTheme)
    {
        string question = GetTextInput("Type the card's question.");

        string answer = GetTextInput("Type the card's answer.");

        InsertCard(stackTheme, question, answer);
    }

    // Need to be done
    private static void StudySessionsMenu(string message = "")
    {
        Console.Clear();

        if (message != "") Console.WriteLine($"\n{message}");
        Console.WriteLine("\nSTUDY SESSIONS\n");
        Console.WriteLine("- Type 1 to Start a Study Session");
        Console.WriteLine("- Type 2 to View Sessions history");
        Console.WriteLine("- Type 0 Return to the main menu");

        switch (GetNumberInput())
        {
            case 0:
                MainMenu();
                break;
            case 1:
                // StartStudySession();
                break;
            case 2:
                // ViewStudySesssionsHistory();
                break;
            default:
                string error = "Wrong input ! Please type a number between 0 and 3";
                StudySessionsMenu(DisplayError(error));
                break;
        }
    }
}
