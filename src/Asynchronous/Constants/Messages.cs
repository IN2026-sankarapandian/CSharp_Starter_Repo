namespace Asynchronous.Constants;

/// <summary>
/// Have constant related to messages.
/// </summary>
public static class Messages
{
    /// <summary>
    /// Title message of app shown to the user.
    /// </summary>
    public const string AppTitle = "Async/Await, Task Parallel Library, and Multi-Threading in C# ";

    /// <summary>
    /// Prompt message shown to the user to enter the desired task.
    /// </summary>
    public const string EnterWhichTaskToRun = "Enter which task to run : ";

    /// <summary>
    /// Error message displayed to the user that input cannot be empty.
    /// </summary>
    public const string InputCannotBeEmpty = "Input cannot be empty";

    /// <summary>
    /// Error message displayed to the user to enter a valid option.
    /// </summary>
    public const string EnterValidOption = "Enter a valid option";

    /// <summary>
    /// Prompt message displayed to the user for exit option.
    /// </summary>
    public const string Exit = "{0}. Exit";

    /// <summary>
    /// Title message of the task 1 displayed to the user.
    /// </summary>
    public const string Task1Title = "Task 1: Understanding and Implementing Async/Await";

    /// <summary>
    /// Prompt message of example URL displayed to the user.
    /// </summary>
    public const string ExampleURL = "Example URl : https://openlibrary.org/search.json?q=the+lord+of+the+rings";

    /// <summary>
    /// Prompt message displayed to the user to enter a url to fetch.
    /// </summary>
    public const string EnterURL = "Enter a url to fetch data : ";

    /// <summary>
    /// Prompt message displayed to the user to press enter to exit.
    /// </summary>
    public const string EnterToExit = "Press enter to exit";

    /// <summary>
    /// Prompt message displayed to the user to inform about data fetching.
    /// </summary>
    public const string FetchingData = "Fetching data....";

    /// <summary>
    /// Title message of task 2 displayed to the user.
    /// </summary>
    public const string Task2Title = "Task 2: Implementing and Understanding Task Parallel Library";

    /// <summary>
    /// Prompt message displayed to the user about time elapsed for calculation using TPL.
    /// </summary>
    public const string ElapsedTimeParallel = "Elapsed time when calculated with parallel : {0}";

    /// <summary>
    /// Prompt message displayed to the user about time elapsed for calculation in sequence.
    /// </summary>
    public const string ElapsedTimeSequence = "Elapsed time when calculated in sequence : {0}";

    /// <summary>
    /// Title message of task 3 displayed to the user,
    /// </summary>
    public const string Task3Title = "Task 3: Advanced Understanding and Usage of Multi-Threading";

    /// <summary>
    /// Prompt message displayed to the user to enter minimum value to generate prime numbers.
    /// </summary>
    public const string EnterMinValueForPrime = "Enter a minimum value to generate prime numbers with : ";

    /// <summary>
    /// Prompt message displayed to the user to enter count value to generate prime numbers.
    /// </summary>
    public const string EnterCountValueForPrime = "Enter a count of prime numbers to generate : ";

    /// <summary>
    /// Prompt message displayed to the user to enter minimum value to generate fibonacci series.
    /// </summary>
    public const string EnterMinValueForFib = "Enter a minimum value to generate fibonacci series with : ";

    /// <summary>
    /// Prompt message displayed to the user to enter count value to generate fibonacci series.
    /// </summary>
    public const string EnterCountValueForFib = "Enter a count of integers for fibonacci series to generate : ";

    /// <summary>
    /// Prompt message displayed to the user to inform no integers were generated.
    /// </summary>
    public const string NoIntegers = "No integers generated within the given range";

    /// <summary>
    /// Prompt message displayed to the user to display result.
    /// </summary>
    public const string Result = "Result : ";

    /// <summary>
    /// Title message of task 4 displayed to the user.
    /// </summary>
    public const string Task4Title = "Task 4: Implementing Multi-Layered Async/Await Operations in a Real-World Scenario";

    /// <summary>
    /// Description message of task 4 displayed to the user.
    /// </summary>
    public const string Task4ToolDescription = "The tool will get the summary of longest word in the file from Wikipedia";

    /// <summary>
    /// Prompt message displayed to the user to enter the path of the file.
    /// </summary>
    public const string EnterPath = "Enter path of the file : ";

    /// <summary>
    /// Prompt message displayed to the user to display summary.
    /// </summary>
    public const string Summary = "Summary : {0}";

    /// <summary>
    /// Prompt message displayed to the user to display error.
    /// </summary>
    public const string Error = "Error : {0}";

    /// <summary>
    /// Prompt message displayed to the user inform file reading process.
    /// </summary>
    public const string ReadingFile = "Reading file";

    /// <summary>
    /// Prompt message displayed to the user that no summary found.
    /// </summary>
    public const string NoSummary = "No summary found.";

    /// <summary>
    /// Title message of task 6 displayed to the user.
    /// </summary>
    public const string Task6Title = "Task 6: Real-World Application of ConfigureAwait in a Console Application with Thread Tracking ";

    /// <summary>
    /// Description message of task 6 displayed to the user.
    /// </summary>
    public const string Task6ToolDescription = "The tool will get the total numbers of words in the file.";

    /// <summary>
    /// Prompt message displayed to the user to display total number of words in file.
    /// </summary>
    public const string TotalWords = "Total no of words in the file is {0}";

    /// <summary>
    /// Error message displayed to the user that file can't be read.
    /// </summary>
    public const string CantReadFile = "File can't be read, error : {0}";

    /// <summary>
    /// Prompt message displayed to the user to display the thread id before await.
    /// </summary>
    public const string ThreadUsedBeforeAwait = "Thread used before await : {0}";

    /// <summary>
    /// Prompt message displayed to the user to display the thread id after await.
    /// </summary>
    public const string ThreadUsedAfterAwait = "Thread used after await : {0}";

    /// <summary>
    /// Title message of task 7 displayed to the user.
    /// </summary>
    public const string Task7Title = "Task 7: Understanding the Difference between Async Void and Async Task with Exceptions";

    /// <summary>
    /// Exception message of void method.
    /// </summary>
    public const string VoidExceptionMessage = "This exception is from void method";

    /// <summary>
    /// Exception message of task return method.
    /// </summary>
    public const string TaskExceptionMessage = "This exception is from task method";

    /// <summary>
    /// Keyword displayed to the user for unknown values.
    /// </summary>
    public const string Unknown = "Unknown";

    /// <summary>
    /// Error message displayed to the user that input URL is not valid.
    /// </summary>
    public const string NotValidURL = "Not a valid URL";

    /// <summary>
    /// Error message displayed to the user that path contain invalid characters.
    /// </summary>
    public const string InvalidCharacters = "Path contain invalid characters";

    /// <summary>
    /// Error message displayed to the user that path is relative.
    /// </summary>
    public const string NotAbsolutePath = "Path must be absolute. ex: C:/asd/asd.txt";

    /// <summary>
    /// Error message displayed to the user that path is not a text file.
    /// </summary>
    public const string NotTxtFile = "Only .txt files are supported";

    /// <summary>
    /// Error message displayed to the user that path doesn't exists.
    /// </summary>
    public const string NoFileExists = "No files exists in specified path";
}
