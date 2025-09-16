using BoilerMachineApp.BoilerMachines;
using BoilerMachineApp.BoilerMachineStatuses;
using BoilerMachineApp.Common;
using BoilerMachineApp.Enums;
using BoilerMachineApp.LogFetchers;
using BoilerMachineApp.Loggers;
using BoilerMachineApp.UserInterface;

namespace BoilerMachineApp;

/// <summary>
/// Its an simulated boiler machine app.
/// </summary>
public class BoilerMachineApp
{
    /// <summary>
    /// Its an entry point of the boiler machine app
    /// </summary>
    public static void Main()
    {
        IUserInterface userInterface = new ConsoleUI();

        // Path of log file to log events.
        string rootPath = AppDomain.CurrentDomain.BaseDirectory;
        string logFilePath = Path.Combine(rootPath, "Boiler Log.txt");
        ILogger logger = new CSVLogger(logFilePath);
        ILogFetcher logFetcher = new CsvLogFetcher(logFilePath);

        BoilerMachine boilerMachine = new (logger);

        // Console subscribe to get status messages
        userInterface.Subscribe(boilerMachine);
        boilerMachine.SetStatus(new BoilerMachineLockoutState(boilerMachine));

        userInterface.ShowMessage(
            MessageType.Information,
            "1. Start boiling\n2. Stop boiling\n3. Simulate Boiler error\n4. Toggle Run Interlock Switch (Open/Closed)\n5. Rest to lockout\n6. View logs\n7. Exit");
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
                    result = boilerMachine.SimulateBoilerError();
                    break;
                case "4":
                    result = boilerMachine.ToggleRunInterlockSwitch();
                    break;
                case "5":
                    result = boilerMachine.ResetLockOut();
                    break;
                case "6":
                    userInterface.ShowLogs(logFetcher.GetEventLogs());
                    break;
                case "7":
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