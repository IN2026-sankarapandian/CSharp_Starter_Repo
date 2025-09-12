using CSharpAdvanced.Constants;
using CSharpAdvanced.Enums;
using CSharpAdvanced.Tasks.PatternMatching.Shapes;
using CSharpAdvanced.UserInterface;

namespace CSharpAdvanced.Tasks.PatternMatching;

/// <summary>
/// Demonstrates the usage and working of pattern matching.
/// </summary>
public class Task7 : ITask
{
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task7"/> class.
    /// </summary>
    /// <param name="userInterface">Provides operations to interact with user.</param>
    public Task7(IUserInterface userInterface) => this._userInterface = userInterface;

    /// <inheritdoc/>
    public string Name => Titles.TaskTitle7;

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);
        this._userInterface.ShowMessage(MessageType.Highlight, Messages.PatternMatchingDescription);
        Shape circle = new Circle()
        {
            Color = "Red",
            Radius = 10,
        };
        Shape rectangle = new Rectangle()
        {
            Color = "Yellow",
            Height = 10,
            Width = 20,
        };
        Shape triangle = new Triangle()
        {
            Color = "Blue",
            Base = 10,
            Height = 15,
        };

        List<Shape> shapes = new () { circle, rectangle, triangle };

        this._userInterface.ShowMessage(
            MessageType.Information,
            Messages.DisplayShapes);
        foreach (Shape shape in shapes)
        {
            this.DisplayShapeDetails(shape);
        }

        this._userInterface.ShowMessage(MessageType.Prompt, string.Format(Messages.TaskExitPrompt, 7));
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Display the details of shape
    /// </summary>
    /// <param name="shape">Shape to display.</param>
    private void DisplayShapeDetails(Shape shape)
    {
        string result = shape switch
        {
            Circle circle => string.Format(Messages.DisplayCircle, circle.Color, circle.Radius, circle.CalculateArea()),
            Rectangle rectangle => string.Format(Messages.DisplayRectangle, rectangle.Color, rectangle.Height, rectangle.Width, rectangle.CalculateArea()),
            Triangle triangle => string.Format(Messages.DisplayTriangle, triangle.Color, triangle.Base, triangle.Height, triangle.CalculateArea()),
            _ => Messages.UnknownShape,
        };

        this._userInterface.ShowMessage(MessageType.Information, result);
    }
}
