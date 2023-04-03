using static FlashCards.Helpers;
using static FlashCards.DataValidation;
using static FlashCards.DataAccess;
using ConsoleTableExt;
using FlashCards.Models;

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
                DisplayStacks();
                
                int stackId = GetNumberInput("\nType the ID of the stack you want to inspect, or type 0 to return to the stack menu.");

                InspectStack(stackId);

                Console.WriteLine("\nPress Enter to go back to the stack menu.");
                Console.ReadLine();
                
                StacksMenu();
                break;
            case 3:
                DisplayStacks();

                stackId = GetNumberInput("\nType the ID of the stack you want to Update, or type 0 to return to the stack menu.");

                while (stackId != 0)
                {
                    UpdateStack(stackId);
                    DisplayStacks();
                    stackId = GetNumberInput("\nType the ID of the stack you want to Update, or type 0 to return to the stack menu.");
                }

                StacksMenu();
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

    private static void UpdateStack(int stackId)
    {
        InspectStack(stackId);

        Console.WriteLine("\nMODIFY STACK\n");
        Console.WriteLine("- Type 1 to Modify the stack's theme");
        Console.WriteLine("- Type 2 to Add a card");
        Console.WriteLine("- Type 3 to Modify a card");
        Console.WriteLine("- Type 4 to Delete a card");
        Console.WriteLine("- Type 0 to go back to the stack menu");

        switch (GetNumberInput())
        {
            case 0:
                StacksMenu();
                break;
            case 1:
                Console.Clear();

                string stackNewTheme = GetTextInput($"\nType the stack's new theme");

                UpdateStackTheme(stackId, stackNewTheme);

                UpdateStack(stackId);
                break;
            case 2:

                break;
            case 3:
                InspectStack(stackId);

                int cardId = GetNumberInput("\nType in the card's id you wish to Update, or type 0 to get back to the stacks menu.\n");

                while (cardId != 0)
                {
                    UpdateCard(stackId, cardId);
                    InspectStack(stackId);
                    cardId = GetNumberInput("\nType in the card's id you wish to Update, or type 0 to get back to the stacks menu.\n");
                }
                break;
            case 4:
                break;
            default:
                string error = "Wrong input ! Please type a number between 0 and 2";
                StacksMenu(DisplayError(error));
                break;
        }
    }

    private static void UpdateCard(int stackId, int cardId)
    {
        int answer = GetNumberInput("Type 1 to update the question, 2 to update the answer or 0 to go back to the menu.");

        while (answer != 0)
        {
            Console.Clear();

            InspectStack(stackId); // change this to inspect the particular card

            switch (answer)
            {
                case 1:
                    string question = GetTextInput("Type the new question");
                    UpdateCardQuestion(cardId, question);
                    break;
                case 2:
                    string questionAnswer = GetTextInput("Type the new answer");
                    UpdateCardAnswer(cardId, questionAnswer);
                    break;
            }

            InspectStack(stackId);

            answer = GetNumberInput("Type 1 to update the question, 2 to update the answer or 0 to go back to the stack.");
        }
    }

    private static void CreateStack()
    {
        Console.WriteLine("\nSTACK CREATION\n");

        string stackTheme = GetTextInput("Type the theme of the new stack, or type 0 to go back to the stack menu.\n");

        if (stackTheme == "0") StacksMenu();

        InsertStack(stackTheme);

        Console.WriteLine("\nTo insert a new card in the stack type 1, else press enter to go back to the stack menu.\n");
        string continueToAddCard = Console.ReadLine();

        while(continueToAddCard == "1")
        {
            AddCard(stackTheme);
            Console.WriteLine("\nType 1 to add another card, else press enter.\n");
            continueToAddCard = Console.ReadLine();
        }

        StacksMenu();
    }

    private static void AddCard(string stackTheme)
    {
        string question = GetTextInput("\nType the card's question.\n");

        string answer = GetTextInput("\nType the card's answer.\n");

        Console.Clear() ;

        InsertCard(stackTheme, question, answer);
    }

    private static void DisplayStacks()
    {
        Console.Clear();

        Console.WriteLine("\nSTACKS\n");

        ConsoleTableBuilder.From(GetStacks()).ExportAndWriteLine();
    }

    private static void InspectStack(int stackId)
    {
        StackCardsDTO stackCards = GetStack(stackId);

        Console.Clear(); 

        Console.WriteLine($"\n{stackCards.Theme.ToUpper()}\n");

        ConsoleTableBuilder.From(stackCards.CardsDTO).ExportAndWriteLine();
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
