using System.Diagnostics;
using Reflections.Constants;
using Reflections.Enums;
using Reflections.Tasks.Task7.Shape;
using Reflections.UserInterface;

namespace Reflections.Tasks.Task7;

/// <summary>
/// It demonstrates the limitation of reflection and efficiency of reflection emit.
/// </summary>
public class SerializationAPI : ITask
{
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="SerializationAPI"/> class.
    /// </summary>
    /// <param name="userInterface"> Provides operations to interact with user.</param>
    public SerializationAPI(IUserInterface userInterface)
    {
        this._userInterface = userInterface;
    }

    /// <inheritdoc/>
    public string Name => KeyWords.SerializationAPITitle;

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);
        Rectangle rectangle = new Rectangle();

        ObjectSerializer serializer = new ObjectSerializer();
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        string result = serializer.SerializeUsingReflection(rectangle);
        stopwatch.Stop();
        this._userInterface.ShowMessage(MessageType.Information, string.Format(PromptMessages.SerializationUsingReflection, stopwatch.ElapsedMilliseconds));

        stopwatch.Restart();
        string result1 = serializer.SerializeUsingOpcode(rectangle);
        stopwatch.Stop();
        this._userInterface.ShowMessage(MessageType.Information, string.Format(PromptMessages.SerializationUsingReflectionEmit, stopwatch.ElapsedMilliseconds));

        stopwatch.Restart();
        string result2 = serializer.SerializeUsingOpcode(rectangle);
        stopwatch.Stop();
        this._userInterface.ShowMessage(MessageType.Information, string.Format(PromptMessages.SerializationUsingReflectionEmit, stopwatch.ElapsedMilliseconds));

        this._userInterface.ShowMessage(MessageType.Prompt, PromptMessages.PressEnterToExit);
        this._userInterface.GetInput();
    }
}
