using MusicalNotes.Constants;

namespace MusicalNotes;

/// <summary>
/// Its an entry point of musical note app.
/// It wil play the sound of each notes for the key pressed by user.
/// </summary>
public class Program
{
    /// <summary>
    /// Its an entry point of musical notes app.
    /// </summary>
    static void Main()
    {
        Console.WriteLine("Key board");
        Console.WriteLine("Press following key to play, press esc to exit");
        Console.WriteLine("Q W  E R  T Y U  I O  P A B");
        Console.WriteLine("C C# D D# E F F# G G# A A# B");
        do
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            // If user press esc app will exit.
            if(keyPressed.Key == ConsoleKey.Escape)
            {
                return;
            }

            BeepNote(keyPressed);
            
        } while (true);
    }

    /// <summary>
    /// It will beep the according to the key info
    /// </summary>
    /// <param name="keyPressed">Key pressed by the user.</param>
    private static void BeepNote(ConsoleKeyInfo keyPressed)
    {
        switch (keyPressed.Key)
        {
            case ConsoleKey.Q:
                Console.Beep(261, NotesResources.Duration);
                break;
            case ConsoleKey.W:
                Console.Beep(277, NotesResources.Duration);
                break;
            case ConsoleKey.E:
                Console.Beep(293, NotesResources.Duration);
                break;
            case ConsoleKey.R:
                Console.Beep(311, NotesResources.Duration);
                break;
            case ConsoleKey.T:
                Console.Beep(329, NotesResources.Duration);
                break;
            case ConsoleKey.Y:
                Console.Beep(369, NotesResources.Duration);
                break;
            case ConsoleKey.U:
                Console.Beep(349, NotesResources.Duration);
                break;
            case ConsoleKey.I:
                Console.Beep(415, NotesResources.Duration);
                break;
            case ConsoleKey.O:
                Console.Beep(415, NotesResources.Duration);
                break;
            case ConsoleKey.P:
                Console.Beep(440, NotesResources.Duration);
                break;
            case ConsoleKey.A:
                Console.Beep(466, NotesResources.Duration);
                break;
            case ConsoleKey.S:
                Console.Beep(493, NotesResources.Duration);
                break;
            default:
                Console.WriteLine("Not a valid key");
                break;
        }
    }
}
