using CollectionsAndGenerics.Constants;
using CollectionsAndGenerics.Enums;
using CollectionsAndGenerics.Task1;
using CollectionsAndGenerics.Task2;
using CollectionsAndGenerics.Task3;
using CollectionsAndGenerics.Task4;
using CollectionsAndGenerics.UserInterfaces;

namespace CollectionsAndGenerics.Managers;

/// <summary>
/// Handles the tasks of assignment <see cref="CollectionAndGenerics"/>.
/// </summary>
public class TaskManager
{
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskManager"/> class.
    /// </summary>
    /// <param name="userInterface">Gives access to the user interface.</param>
    public TaskManager(IUserInterface userInterface)
    {
        this._userInterface = userInterface;
    }

    /// <summary>
    /// Show menu page to the user.
    /// </summary>
    public void ShowMenu()
    {
        while (true)
        {
            this._userInterface.ShowMessage(MessageType.Title, Messages.ColllectionsAndGenrericsTitle);
            this._userInterface.ShowMessage(MessageType.Information, Messages.AvailabletasksPrompt);
            this._userInterface.ShowMessage(MessageType.Prompt, Messages.EnterTask);
            string? userChoice = this._userInterface.GetInput();
            switch (userChoice)
            {
                case "1":
                    this.ExecuteTask1();
                    break;
                case "2":
                    this.ExecuteTask2();
                    break;
                case "3":
                    this.ExecuteTask3();
                    break;
                case "4":
                    this.ExecuteTask4();
                    break;
                case "6":
                    this.ExecuteTask6();
                    break;
                case "7":
                    return;
                default:
                    this._userInterface.ShowMessage(MessageType.Warning, Messages.EnterValidInput);
                    break;
            }
        }
    }

    /// <summary>
    /// Executes task 1
    /// </summary>
    private void ExecuteTask1()
    {
        this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.TaskTitle, 1));
        BookList<string> bookList = new ()
                {
                    "The sapiens",
                    "The secret history",
                    "Ponniyan Selvan",
                    "Crime and punishment",
                    "White nights",
                };

        bookList.Remove("Ponniyan Selvan");

        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.DoItemExistInList, "Ponniyan Selvan", nameof(bookList), bookList.Contains("Ponniyan Selvan")));

        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.BookTitlesInTheList, nameof(bookList)));
        foreach (string item in bookList)
        {
            this._userInterface.ShowMessage(MessageType.Information, item);
        }

        this._userInterface.ShowMessage(MessageType.Prompt, string.Format(Messages.PressEnterToExitTask, 1));
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Executes task 2
    /// </summary>
    private void ExecuteTask2()
    {
        this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.TaskTitle, 2));
        CustomStack<char> characters = new ();

        string? sampleString = "sample string";
        foreach (char character in sampleString!)
        {
            characters.Push(character);
        }

        string reversedSampleString = string.Empty;

        while (characters.HasElement())
        {
            reversedSampleString += characters.Pop();
        }

        this._userInterface.ShowMessage(MessageType.Result, string.Format(Messages.ReversedStringOfSampleString, sampleString, reversedSampleString));
        this._userInterface.ShowMessage(MessageType.Prompt, string.Format(Messages.PressEnterToExitTask, 2));
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Executes task 3
    /// </summary>
    private void ExecuteTask3()
    {
        this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.TaskTitle, 3));

        PeopleQueue<string> peopleQueue = new ();

        peopleQueue.Enqueue("Sankar");
        peopleQueue.Enqueue("Arthur");
        peopleQueue.Enqueue("Guru");
        peopleQueue.Enqueue("Uttand");
        peopleQueue.Enqueue("Harish");

        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.PeoplesInQueue, nameof(peopleQueue)));
        foreach (string people in peopleQueue)
        {
            this._userInterface.ShowMessage(MessageType.Information, people);
        }

        while (peopleQueue.HasPeople())
        {
            peopleQueue.Dequeue();
        }

        this._userInterface.ShowMessage(MessageType.Prompt, string.Format(Messages.PressEnterToExitTask, 3));
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Executes task 4
    /// </summary>
    private void ExecuteTask4()
    {
        this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.TaskTitle, 4));

        StudentDictionary<string, int> studentDictionary = new ()
                {
                    { "Sankar", 1 },
                    { "Arthur", 2 },
                    { "Guru", 3 },
                    { "Uttand", 4 },
                    { "Harish", 5 },
                };

        studentDictionary.Remove("Harish");

        foreach (KeyValuePair<string, int> student in studentDictionary)
        {
            this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.NameGrade, student.Key, student.Value));
        }

        this._userInterface.ShowMessage(MessageType.Prompt, string.Format(Messages.PressEnterToExitTask, 4));
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Executes task 6
    /// </summary>
    private void ExecuteTask6()
    {
        this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.TaskTitle, 6));

        List<int> integerList = new () { 1, 2, 3, 4, 5 };
        int[] integerArray = new int[] { 1, 2, 3, 4, 5 };
        Stack<int> integerStack = new (new[] { 1, 2, 3, 4, 5 });

        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.SumOfIntegerList, this.SumOfElements(integerList)));
        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.SumOfIntegerArray, this.SumOfElements(integerArray)));
        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.SumOfIntegerStack, this.SumOfElements(integerStack)));

        IReadOnlyDictionary<string, int> keyValuePairs = this.GenerateDictionary();
        this.PrintDictionary(keyValuePairs);

        /// This line throws a error as the <see cref="IReadOnlyDictionary{TKey, TValue}"/> was attempted edit.
        // keyValuePairs["one"] = 2;
        this._userInterface.ShowMessage(MessageType.Prompt, string.Format(Messages.PressEnterToExitTask, 6));
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Computes the sum of specified integers.
    /// </summary>
    /// <param name="integers">Integers to sum.</param>
    /// <returns>Sum of the integers</returns>
    private int SumOfElements(IEnumerable<int> integers)
    {
        int sum = 0;
        foreach (int integer in integers)
        {
            sum += integer;
        }

        return sum;
    }

    /// <summary>
    /// It generates a dictionary with some sample values.
    /// </summary>
    /// <returns>Dictionary with sample values.</returns>
    private IReadOnlyDictionary<string, int> GenerateDictionary()
    {
        IReadOnlyDictionary<string, int> keyValuePairs = new Dictionary<string, int>()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
        };
        return keyValuePairs;
    }

    /// <summary>
    /// It generates a dictionary with some sample values.
    /// </summary>
    /// <param name="keyValuePairs">Key  and value pairs to print.</param>
    private void PrintDictionary(IReadOnlyDictionary<string, int> keyValuePairs)
    {
        foreach (KeyValuePair<string, int> keyValuePair in keyValuePairs)
        {
            this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.KeyAndValue, keyValuePair.Key, keyValuePair.Value));
        }
    }
}
