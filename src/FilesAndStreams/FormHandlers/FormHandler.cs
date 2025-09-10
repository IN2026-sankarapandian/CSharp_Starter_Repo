using FilesAndStreams.Common;
using FilesAndStreams.Constants;
using FilesAndStreams.Enums;
using FilesAndStreams.UserInterface;
using FilesAndStreams.Validators;

namespace FilesAndStreams.FormHandlers;

/// <summary>
/// Provides methods to get required data from user.
/// </summary>
public class FormHandler
{
    private readonly IUserInterface _userInterface;
    private readonly FileValidator _fileValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="FormHandler"/> class.
    /// </summary>
    /// <param name="userInterface">Provide method to interact with user via UI.</param>
    /// <param name="fileValidator">Provide method to validate file related data.</param>
    public FormHandler(IUserInterface userInterface, FileValidator fileValidator)
    {
        this._userInterface = userInterface;
        this._fileValidator = fileValidator;
    }

    /// <summary>
    /// Gets a valid path to save a text file from the user.
    /// </summary>
    /// <param name="prompt">Prompt to be shown to user.</param>
    /// <returns>Valid file path from user.</returns>
    public string GetTxtFileSavePath(string prompt)
    {
        do
        {
            string filePath = this.GetUserInput(prompt);
            Result<bool> isValidTxtFilePathResult = this._fileValidator.IsValidTextFileSavePath(filePath);
            if (!isValidTxtFilePathResult.IsSuccess && !isValidTxtFilePathResult.Value)
            {
                this._userInterface.ShowMessage(MessageType.Warning, isValidTxtFilePathResult.ErrorMessage);
            }
            else
            {
                return filePath;
            }
        }
        while (true);
    }

    /// <summary>
    /// Gets the temperature threshold from user.
    /// </summary>
    /// <param name="prompt">Prompt to be shown to user</param>
    /// <returns>Temperature threshold given by user</returns>
    public decimal GetTemperatureThreshold(string prompt)
    {
        do
        {
            string userInput = this.GetUserInput(prompt);
            if (!decimal.TryParse(userInput, out decimal temperatureThreshold))
            {
                this._userInterface.ShowMessage(MessageType.Warning, Messages.NotValidTemperature);
            }
            else if (temperatureThreshold >= 150 || temperatureThreshold <= 50)
            {
                this._userInterface.ShowMessage(MessageType.Warning, Messages.TemperatureRangeMismatches);
            }
            else
            {
                return temperatureThreshold;
            }
        }
        while (true);
    }

    /// <summary>
    /// Gets a valid path of txt file from the user.
    /// </summary>
    /// <param name="prompt">Prompt to be shown for the user.</param>
    /// <returns>Valid file path from user.</returns>
    public string GetTxtFilePath(string prompt)
    {
        do
        {
            string filePath = this.GetUserInput(prompt);
            Result<bool> isValidTxtFilePathResult = this._fileValidator.IsValidTextFilePath(filePath);
            if (!isValidTxtFilePathResult.IsSuccess && !isValidTxtFilePathResult.Value)
            {
                this._userInterface.ShowMessage(MessageType.Warning, isValidTxtFilePathResult.ErrorMessage);
            }
            else
            {
                return filePath;
            }
        }
        while (true);
    }

    /// <summary>
    /// Gets a input from user.
    /// </summary>
    /// <param name="prompt">Prompt to be shown.</param>
    /// <returns>User's input given for the prompt.</returns>
    public string GetUserInput(string prompt)
    {
        do
        {
            this._userInterface.ShowMessage(MessageType.Prompt, prompt);
            string? userInput = this._userInterface.GetInput();
            if (string.IsNullOrEmpty(userInput))
            {
                this._userInterface.ShowMessage(MessageType.Warning, Messages.InputCannotBeEmpty);
            }
            else
            {
                return userInput;
            }
        }
        while (true);
    }
}
