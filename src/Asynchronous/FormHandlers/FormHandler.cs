using Asynchronous.Common;
using Asynchronous.Enums;
using Asynchronous.UserInterface;
using Asynchronous.Validators;

namespace Asynchronous.FormHandlers;

/// <summary>
/// Provide methods to get data required by the tasks of this assignment.
/// </summary>
public class FormHandler
{
    private readonly IUserInterface _userInterface;
    private readonly Validator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="FormHandler"/> class.
    /// </summary>
    /// <param name="userInterface">Provide method to interact with user via UI</param>
    /// <param name="validator">Provide methods to validate user data.</param>
    public FormHandler(IUserInterface userInterface, Validator validator)
    {
        this._userInterface = userInterface;
        this._validator = validator;
    }

    /// <summary>
    /// Gets a valid URL path from the user.
    /// </summary>
    /// <param name="prompt">Prompt to be shown to the user.</param>
    /// <returns>URL path from the user.</returns>
    public string GetUrl(string prompt)
    {
        do
        {
            string userInputURL = this.GetUserInput(prompt);
            Result<bool> validatedResult = this._validator.ValidateURL(userInputURL);
            if (validatedResult.IsSuccess && validatedResult.Value)
            {
                return userInputURL;
            }

            this._userInterface.ShowMessage(MessageType.Warning, "Not a valid URL");
        }
        while (true);
    }

    /// <summary>
    /// Gets the non empty input from the user.
    /// </summary>
    /// <param name="prompt">Prompt to be shown to the user.</param>
    /// <returns>Input given by the user as string.</returns>
    public string GetUserInput(string prompt)
    {
        do
        {
            this._userInterface.ShowMessage(MessageType.Prompt, prompt);
            string? userInput = this._userInterface.GetInput();

            if (!string.IsNullOrEmpty(userInput))
            {
                return userInput;
            }
        }
        while (true);
    }

    /// <summary>
    /// Gets a valid path of txt file from the user.
    /// </summary>
    /// <param name="prompt">Prompt to be shown to the user.</param>
    /// <returns>Valid file path from user.</returns>
    public string GetTxtFilePath(string prompt)
    {
        do
        {
            string filePath = this.GetUserInput(prompt);
            Result<bool> isValidTxtFilePathResult = this._validator.IsValidTextFilePath(filePath);
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
    /// Gets the valid integer from user.
    /// </summary>
    /// <param name="prompt">Prompt to be shown to the user.</param>
    /// <returns>Integer given by the user.</returns>
    public int GetInteger(string prompt)
    {
        do
        {
            string userInput = this.GetUserInput(prompt);
            if (int.TryParse(userInput, out int integer))
            {
                return integer;
            }

            this._userInterface.ShowMessage(MessageType.Warning, "Not a valid integer");
        }
        while (true);
    }
}
