using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using static FlashCards.Helpers;


namespace FlashCards;

public static class DataAccess
{
    static string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
    static string dbFilePath = ConfigurationManager.AppSettings.Get("DbFilePath");
    static string dbName = ConfigurationManager.AppSettings.Get("DbName");

    public static void InitializeDatabase()
    {
        CreateDb();
        CreateTables();
    }

    private static void CreateDb()
    {
        SqlConnection connection = new SqlConnection(connectionString);

        string sqlString =
            $@"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{dbName}')
            BEGIN
                CREATE DATABASE {dbName} ON PRIMARY
                (NAME = {dbName}_Data, FILENAME = '{dbFilePath}{dbName}.mdf',
                SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)
                LOG ON (NAME = {dbName}_log,
                FILENAME = '{dbFilePath}{dbName}.ldf',
                SIZE = 1MB, MAXSIZE = 5MB, FILEGROWTH = 10%)
            END";

        SqlCommand sqlCommand = new SqlCommand(sqlString, connection);

        try
        {
            connection.Open();
            sqlCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}\n\nPress Enter to continue.");
            Console.ReadLine();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }

    public static void CreateTables()
    {
        SqlConnection connection = new SqlConnection(connectionString);

        string createStacksTable =
            $@"IF NOT EXISTS (SELECT * FROM sysobjects
            WHERE name='Stacks' and xtype='U')
            CREATE TABLE Stacks (
                StackID int NOT NULL IDENTITY PRIMARY KEY,
                Theme nvarchar(50) NOT NULL)";

        SqlCommand sqlCommandStacks = new SqlCommand(createStacksTable, connection);

        string createCardsTable =
            $@"IF NOT EXISTS (SELECT * FROM sysobjects
            WHERE name='Cards' and xtype='U')
            CREATE TABLE Cards (
                CardID int NOT NULL IDENTITY PRIMARY KEY,
                Question Text NOT NULL,
                Answer Text NOT NULL,
                StackID int NOT NULL,
                CONSTRAINT FK_StackCard FOREIGN KEY (StackID)
                REFERENCES Stacks(StackID)
                ON DELETE CASCADE
                ON UPDATE CASCADE)";

        SqlCommand sqlCommandCards = new SqlCommand(createCardsTable, connection);

        string createStudySessionsTable =
            $@"IF NOT EXISTS (SELECT * FROM sysobjects
            WHERE name='StudySessions' and xtype='U')
            CREATE TABLE StudySessions (
                StudySessionsID int NOT NULL IDENTITY PRIMARY KEY,
                Date Text NOT NULL,
                Score Text NOT NULL,
                StackID int NOT NULL,
                CONSTRAINT FK_StackStudySession FOREIGN KEY (StackID)
                REFERENCES Stacks(StackID)
                ON DELETE CASCADE
                ON UPDATE CASCADE)";

        SqlCommand sqlCommandStudySessions = new SqlCommand(createStudySessionsTable, connection);

        try
        {
            connection.Open();

            sqlCommandStacks.ExecuteNonQuery();
            sqlCommandCards.ExecuteNonQuery();
            sqlCommandStudySessions.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}\n\nPress Enter to continue.");
            Console.ReadLine();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }

    public static void InsertStack(string theme)
    {
        theme = SafeTextSql(theme);

        SqlConnection connection = new SqlConnection(connectionString);

        string sqlString =
            $@"IF NOT EXISTS (SELECT * FROM Stacks
            WHERE theme='{theme}')
            INSERT INTO Stacks(Theme) VALUES('{theme}')";

        SqlCommand sqlCommand = new SqlCommand(sqlString, connection);

        try
        {
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            Console.WriteLine($"\nThe stack {theme} has been created successfully !\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}\n\nPress Enter to continue.");
            Console.ReadLine();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }

    public static void InsertCard(string stackName, string question, string answer)
    {
        stackName = SafeTextSql(stackName);
        question = SafeTextSql(question);
        answer = SafeTextSql(answer);

        SqlConnection connection = new SqlConnection(connectionString);

        int stackID = GetStackId(stackName);

        string sqlString =
            $@"INSERT INTO Cards(Question, Answer, StackID)
            VALUES('{question}', '{answer}', {stackID})";

        SqlCommand sqlCommand = new SqlCommand(sqlString, connection);

        try
        {
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            Console.WriteLine($"\nCard added successfully !\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}\n\nPress Enter to continue.");
            Console.ReadLine();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }

    private static int GetStackId(string stackTheme)
    {
        stackTheme = SafeTextSql(stackTheme);

        int stackId = 0;

        SqlConnection connection = new SqlConnection(connectionString);

        string sqlString =
            $@"SELECT StackID FROM Stacks
            WHERE theme='{stackTheme}'";

        SqlCommand sqlCommand = new SqlCommand(sqlString, connection);

        try
        {
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while(reader.Read())
            {
                stackId = reader.GetInt32(0);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}\n\nPress Enter to continue.");
            Console.ReadLine();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        return stackId;
    }
}
