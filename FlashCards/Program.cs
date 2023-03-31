using static FlashCards.Menus;
using static FlashCards.DataAccess;
// First the app should create a database if there is none
// Then create the tables Stacks and FlashCards if they don't exist
// Create DB 
InitDb();

MainMenu("Welcome to the flashcard app !");
