using Reflections.Constants;
using Reflections.Enums;
using Reflections.Handlers;
using Reflections.Helpers;
using Reflections.Tasks.Task6.Calculator;
using Reflections.Tasks.Task6.TypeBuilders;
using Reflections.UserInterface;

namespace Reflections.Tasks.Task6;

/// <summary>
/// It tests the methods of dynamically created object of <see cref="ICalculator"/>.
/// </summary>
public class MockingFrameworkTask : ITask
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandler _formHandler;
    private readonly AssemblyHelper _assemblyHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="MockingFrameworkTask"/> class.
    /// </summary>
    /// <param name="userInterface"> Provides operations to interact with user.</param>
    /// <param name="formHandler"> Gets required data from user.</param>
    /// <param name="assemblyHelper">Provide helper methods to manipulate and work with assembly and it related types.</param>
    public MockingFrameworkTask(IUserInterface userInterface, FormHandler formHandler, AssemblyHelper assemblyHelper)
    {
        this._userInterface = userInterface;
        this._formHandler = formHandler;
        this._assemblyHelper = assemblyHelper;
    }

    /// <inheritdoc/>
    public string Name => KeyWords.MockingFrameworkTitle;

    /// <inheritdoc/>
    public void Run()
    {
        this.HandleTestingMenu();
    }

    private void HandleTestingMenu()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);
        ICalculator? instance = this.CreateCalculatorInstance();
        this._userInterface.ShowMessage(MessageType.Title, this.Name);
        this._userInterface.ShowMessage(MessageType.Information, PromptMessages.MockingFrameworkMenuOptions);
        string userChoice = this._formHandler.GetUserInput(PromptMessages.EnterWhichTestToRun);
        switch (userChoice)
        {
            case "1":
                this.HandleTestAddMethod(instance);
                this._userInterface.ShowMessage(MessageType.Prompt, PromptMessages.PressEnterToExit);
                this._userInterface.GetInput();
                break;
            case "2":
                this.HandleTestSubtractMethod(instance);
                this._userInterface.ShowMessage(MessageType.Prompt, PromptMessages.PressEnterToExit);
                this._userInterface.GetInput();
                break;
            case "3":
                return;
            default:
                break;
        }
    }

    /// <summary>
    /// Tests the add method of the <see cref="ICalculator"/> type.
    /// </summary>
    /// <param name="instance">Instance of <see cref="ICalculator"/>.</param>
    private void HandleTestAddMethod(ICalculator? instance)
    {
        if (instance is null)
        {
            return;
        }

        int sum = instance.Add(5, 3);

        if (sum == 8)
        {
            this._userInterface.ShowMessage(MessageType.Information, PromptMessages.SumTestPassed);
        }
        else
        {
            this._userInterface.ShowMessage(MessageType.Information, PromptMessages.SumTestFailed);
        }
    }

    /// <summary>
    /// Tests the subtract method of the <see cref="ICalculator"/> type.
    /// </summary>
    /// <param name="instance">Instance of <see cref="ICalculator"/>.</param>
    private void HandleTestSubtractMethod(ICalculator? instance)
    {
        if (instance is null)
        {
            return;
        }

        int difference = instance.Subtract(10, 4);

        if (difference == 6)
        {
            this._userInterface.ShowMessage(MessageType.Information, PromptMessages.SubTestPassed);
        }
        else
        {
            this._userInterface.ShowMessage(MessageType.Information, PromptMessages.SubTestFailed);
        }
    }

    /// <summary>
    /// Creates a new instance of <see cref="ICalculator"/> type.
    /// </summary>
    /// <returns>Created instance of <see cref="ICalculator"/>.</returns>
    private ICalculator? CreateCalculatorInstance()
    {
        CalculatorTypeBuilder calculatorTypeBuilder = new CalculatorTypeBuilder();
        Type? calculatorType = calculatorTypeBuilder.BuildCalculatorType();
        Result<object> calculatorTypeResult = this._assemblyHelper.CreateTypeInstance(calculatorType);

        if (calculatorTypeResult.IsSuccess)
        {
            return (ICalculator)calculatorTypeResult.Value;
        }

        return null;
    }
}
