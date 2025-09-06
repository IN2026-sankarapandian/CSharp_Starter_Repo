using MockingFramework.Tasks;
using Reflections.Tasks;
using Reflections.UserInterface;

namespace MockingFramework;

public class Program
{
    public static void Main(string[] args)
    {
        IUserInterface userInterface = new ConsoleUI();

        ITask task = new MockingFrameworkTask(userInterface);
        task.Run();
    }
}