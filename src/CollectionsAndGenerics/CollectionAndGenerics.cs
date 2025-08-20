using CollectionsAndGenerics.Task1;
using CollectionsAndGenerics.Task2;
using CollectionsAndGenerics.Task3;
using CollectionsAndGenerics.Task4;

namespace CollectionsAndGenerics;

/// <summary>
/// Demonstrates the generics.
/// </summary>
public class CollectionAndGenerics
{
    /// <summary>
    /// Its an entry point of the <see cref="CollectionAndGenerics"/>.
    /// </summary>
    public static void Main()
    {
        // Task 1
        Console.WriteLine("Task 1");
        BookList<string> bookList = new ()
        {
            "The sapiens",
            "The secret history",
            "Ponniyan Selvan",
            "Crime and punishment",
            "White nights",
        };

        bookList.Remove("Ponniyan Selvan");

        Console.WriteLine("Do {0} exists in the {1} : {2}", "Ponniyan Selvan", nameof(bookList), bookList.Contains("Ponniyan Selvan"));

        Console.WriteLine("Book titles in the {0} : ", nameof(bookList));
        foreach (string item in bookList)
        {
            Console.WriteLine(item);
        }

        // Task 2
        Console.WriteLine("\n\nTask 2");
        CustomStack<char> characters = new ();

        string? userInput;
        do
        {
            Console.Write("Enter a string to reverse : ");
            userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Input cannot be empty !");
            }
            else
            {
                foreach (char character in userInput!)
                {
                    characters.Add(character);
                }
            }
        }
        while (string.IsNullOrEmpty(userInput));

        string reversedString = string.Empty;

        while (characters.HasElement())
        {
            reversedString += characters.Pop();
        }

        Console.WriteLine("The reversed string of given input {0} is : {1}", userInput, reversedString);

       // Task 3
        Console.WriteLine("\n\nTask 3");

        PeopleQueue<string> peopleQueue = new ();

        peopleQueue.Enqueue("Sankar");
        peopleQueue.Enqueue("Arthur");
        peopleQueue.Enqueue("Guru");
        peopleQueue.Enqueue("Uttand");
        peopleQueue.Enqueue("Harish");

        Console.WriteLine("Peoples in the {0}", nameof(peopleQueue));
        foreach (string people in peopleQueue)
        {
            Console.WriteLine(people);
        }

        while (peopleQueue.HasPeople())
        {
            peopleQueue.Dequeue();
        }

        // Task 4
        Console.WriteLine("\n\nTask 4");

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
            Console.WriteLine("Name : {0}, Grade : {1}", student.Key, student.Value);
        }

        // Task 6.1
        Console.WriteLine("\n\nTask 6.1");

        List<int> integerList = new () { 1, 2, 3, 4, 5 };
        int[] integerArray = new int[] { 1, 2, 3, 4, 5 };
        Stack<int> integerStack = new (new[] { 1, 2, 3, 4, 5 });

        Console.WriteLine("Sum of integer list : {0}", SumOfElements(integerList));
        Console.WriteLine("Sum of integer array : {0}", SumOfElements(integerArray));
        Console.WriteLine("Sum of integer stack : {0}", SumOfElements(integerStack));

        // Task 6.2
        Console.WriteLine("\n\nTask 6.2");
        IReadOnlyDictionary<string, int> keyValuePairs = GenerateDictionary();
        PrintDictionary(keyValuePairs);

        /// This line throws a error as the <see cref="IReadOnlyDictionary{TKey, TValue}"/> was attempted edit.
        // keyValuePairs["one"] = 2;
        Console.ReadKey();
    }

    /// <summary>
    /// Computes the sum of specified integers.
    /// </summary>
    /// <param name="integers">Integers to sum.</param>
    /// <returns>Sum of the integers</returns>
    public static int SumOfElements(IEnumerable<int> integers)
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
    public static IReadOnlyDictionary<string, int> GenerateDictionary()
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
    public static void PrintDictionary(IReadOnlyDictionary<string, int> keyValuePairs)
    {
        foreach (KeyValuePair<string, int> keyValuePair in keyValuePairs)
        {
            Console.WriteLine("Key : {0}, Value : {1}", keyValuePair.Key, keyValuePair.Value);
        }
    }
}