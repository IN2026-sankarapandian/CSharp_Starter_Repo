using BoilerMachineApp.BoilerMachines;
using BoilerMachineApp.BoilerMachineStatuses;
using BoilerMachineApp.Common;
using BoilerMachineApp.Enums;
using BoilerMachineApp.Loggers;
using BoilerMachineApp.UserInterface;

namespace BoilerMachineApp;

/// <summary>
/// Its an simulated boiler machine app.
/// </summary>
public class Program
{
    /// <summary>
    /// Its an entry point of the boiler machine app
    /// </summary>
    public static void Main()
    {
        IUserInterface userInterface = new ConsoleUI();

        // Path of log file to log events.
        string rootPath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(rootPath, " Boiler Log.txt");
        ILogger logger = new CSVLogger(filePath);

        BoilerMachine boilerMachine = new (logger);

        // Console subscribe to get status messages
        userInterface.Subscribe(boilerMachine);
        boilerMachine.SetStatus(new BoilerMachineReadyState(boilerMachine));

        userInterface.ShowMessage(
            MessageType.Information,
            "1. Start boiling\n2. Stop boiling\n3. Exit");
        do
        {
            userInterface.ShowMessage(MessageType.Prompt, "What do you want to do : ");
            string? userChoice = Console.ReadLine();
            Result? result = null;
            switch (userChoice)
            {
                case "1":
                    result = boilerMachine.StartBoiling();
                    break;
                case "2":
                    result = boilerMachine.StopBoiling();
                    break;
                case "3":
                    return;
                default:
                    userInterface.ShowMessage(MessageType.Error, "Enter a valid option");
                    break;
            }

            if (result is null)
            {
                continue;
            }

            if (result.IsSuccess)
            {
                userInterface.ShowMessage(MessageType.Success, result.SuccessMessage);
            }
            else
            {
                userInterface.ShowMessage(MessageType.Error, result.ErrorMessage);
            }
        }
        while (true);
    }
}