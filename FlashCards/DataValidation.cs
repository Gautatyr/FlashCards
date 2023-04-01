using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FlashCards.Helpers;

namespace FlashCards;

internal class DataValidation
{
    public static int GetNumberInput(string message = "")
    {
        if (message != "") Console.WriteLine(message);

        string userInput = Console.ReadLine();
        int number;

        while (!int.TryParse(userInput, out number)) 
        {
            string error = $"{userInput} is not a valid number. Please try again.";
            Console.WriteLine(DisplayError(error));
            userInput = Console.ReadLine();
        }
        return number;
    }

    public static string GetTextInput(string message = "")
    {
        Console.WriteLine(message);
        string textInput = Console.ReadLine();

        while(string.IsNullOrEmpty(textInput))
        {
            Console.WriteLine(DisplayError("Input can't be empty !"));
            Console.WriteLine(message);
            textInput = Console.ReadLine();
        }

        return textInput;
    }
}
