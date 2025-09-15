namespace FilesAndStreams.Constants;

/// <summary>
/// Contains messages displayed to the user.
/// </summary>
public static class Messages
{
    /// <summary>
    /// Title message of the assignment displayed to the user.
    /// </summary>
    public const string FileAndStreams = "Files and streams";

    /// <summary>
    /// Prompt message displayed to the user about available tasks in files and streams assignment.
    /// </summary>
    public const string TaskOptions = "1. Task 1\n2. Task 2\n3. Task 3\n4. Task 4\n5. Exit";

    /// <summary>
    /// Title message displayer to the user with the place holder of the particular task.
    /// </summary>
    public const string TaskTitle = "Task {0}";

    /// <summary>
    /// Prompt message displayed to the user about available options in task 1.
    /// </summary>
    public const string Task1MenuPrompt =
        "1. Create sample files\n" +
        "2. Read sample files\n" +
        "3. Process sample files\n" +
        "4. Exit";

    /// <summary>
    /// Prompt message displayed to the user about available options in task 2.
    /// </summary>
    public const string Task2MenuPrompt =
        "1. Create sample files (async)\n" +
        "2. Read sample files (async)\n" +
        "3. Process sample files (async)\n" +
        "4. Exit";

    /// <summary>
    /// Prompt message displayed to the user to enter the desired action choice.
    /// </summary>
    public const string AskUserChoice = "Enter what do you want to do : ";

    /// <summary>
    /// Error message displayed to the user to enter a valid option.
    /// </summary>
    public const string EnterValidOption = "Enter a valid option";

    /// <summary>
    /// Title message for the file creating process.
    /// </summary>
    public const string Create = "Create";

    /// <summary>
    /// Title message for the asynchronous file creating process.
    /// </summary>
    public const string CreateAsync = "Create async";

    /// <summary>
    /// Prompt message displayed to the user to enter a path to save the sample file.
    /// </summary>
    public const string EnterPathToSaveFile = "Enter a path to save sample file : ";

    /// <summary>
    /// Title message for the file reading process.
    /// </summary>
    public const string Read = "Read";

    /// <summary>
    /// Title message for the asynchronous file reading process.
    /// </summary>
    public const string ReadAsync = "Read async";

    /// <summary>
    /// Prompt message displayed to the user to enter a path to read the sample file.
    /// </summary>
    public const string EnterPathToReadFile = "Enter a path of sample file to read : ";

    /// <summary>
    /// Prompt message displayed to the user inform press enter to exit the current task.,
    /// </summary>
    public const string PressEnterToExit = "Press enter to exit";

    /// <summary>
    /// Title message for the processing the file.
    /// </summary>
    public const string Process = "Process";

    /// <summary>
    /// Title message for the asynchronous processing the file.
    /// </summary>
    public const string ProcessAsync = "Process async";

    /// <summary>
    /// Prompt message displayed to the user to enter the path of the file to filter the temperature.
    /// </summary>
    public const string EnterPathToFilterFile = "Enter a path of file to filter with temperature : ";

    /// <summary>
    /// Prompt message displayed to the user to save the filtered data.
    /// </summary>
    public const string EnterPathToSaveFilteredFile = "Enter a path to save filtered file : ";

    /// <summary>
    /// Prompt message displayed to the user enter the higher threshold temperature to filter.
    /// </summary>
    public const string EnterTemperatureThresholdValue = "Enter a higher threshold temperature to filter : ";

    /// <summary>
    /// Error message displayed to the user to prompt the error message
    /// in placeholder and press enter to go back to menu
    /// </summary>
    public const string PromptErrorAndGoBack = "{0}. Please press enter to go back to menu ...";

    /// <summary>
    /// Title message of the task that writes a file with placeholder to put file name.
    /// </summary>
    public const string WritingFile = "Writing {0}";

    /// <summary>
    /// Prompt message displayed to the user about available streams to read.
    /// </summary>
    public const string FileReadOptions = "1. File stream\n2. Buffered stream";

    /// <summary>
    /// Prompt message displayed to the user to enter the desired stream to read with.
    /// </summary>
    public const string EnterWhichStreamToUse = "Enter which stream to use to read file : ";

    /// <summary>
    /// Title message for the task that reads a file using file stream with placeholder to put file name.
    /// </summary>
    public const string ReadingFileWithFileStream = "Reading {0} with file stream";

    /// <summary>
    /// Title message for the task that reads a file using buffered stream with placeholder to put file name.
    /// </summary>
    public const string ReadingFileWithBufferedStream = "Reading {0} with buffered stream";

    /// <summary>
    /// Title message for the task that reads the file for filtering with placeholder to put the file name.
    /// </summary>
    public const string ReadingFileForFiltering = "Reading {0} for filtering";

    /// <summary>
    /// Title message for the task that filters the file with placeholder to put the file name.
    /// </summary>
    public const string FilteringFile = "Filtering {0}";

    /// <summary>
    /// Title message for the task that writes the filtered data to the file with placeholder to put the file name.
    /// </summary>
    public const string WritingFilteredFile = "Writing filtered data to {0}";

    /// <summary>
    /// Prompt message displayed to the user to enter some value to write in the file.
    /// </summary>
    public const string EnterValue = "Enter some value to write in the file : ";

    /// <summary>
    /// Prompt message displayed to the user to display elapsed time to write the file.
    /// </summary>
    public const string ElapsedTime = "Elapsed time to write {0}";

    /// <summary>
    /// Prompt message displayed to the user to inform the error were simulated
    /// successfully and check bin for the error log file
    /// </summary>
    public const string ErrorLogFilesSimulated = "Error were simulated and logged, check bin for the log file...";

    /// <summary>
    /// Error message displayed to the user when user entered the invalid temperature format for filtering.
    /// </summary>
    public const string NotValidTemperature = "Not a valid temperature";

    /// <summary>
    /// Error message displayed o the user when user when user enters the temperature
    /// that is not in the applicable range.
    /// </summary>
    public const string TemperatureRangeMismatches = "Threshold must be between 50 C and 150 C";

    /// <summary>
    /// Error message displayed to the user when user enters empty input.
    /// </summary>
    public const string InputCannotBeEmpty = "Input cannot be empty";
}
