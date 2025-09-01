using CSharpAdvanced.Constants;
using CSharpAdvanced.Enums;
using CSharpAdvanced.UserInterface;

namespace CSharpAdvanced.Tasks.Records;

/// <summary>
/// Demonstrates the usage and working of records.
/// </summary>
public class Task6 : ITask
{
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task6"/> class
    /// </summary>
    /// <param name="userInterface">Provides operations to interact with user via console.</param>
    public Task6(IUserInterface userInterface) => this._userInterface = userInterface;

    /// <inheritdoc/>
    public string Name => "Records";

    /// <summary>
    /// Will contain book details as record.
    /// </summary>
    /// <param name="title">Title of the book.</param>
    /// <param name="author">Author of the book.</param>
    /// <param name="isbn">ISBN of book</param>
    public record Book(string title, string author, string isbn);

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);
        this._userInterface.ShowMessage(MessageType.Highlight, Messages.RecordsDescription);

        Book theSecretHistory = new ("The secret history", "Donna Tartt", "1234567890");
        Book sapiens = new ("Sapiens", "Yuval Noah", "0987654321");

        this._userInterface.ShowMessage(MessageType.Information, Messages.Books);
        this._userInterface.ShowMessage(MessageType.Information, theSecretHistory.ToString());
        this._userInterface.ShowMessage(MessageType.Information, sapiens.ToString());

        // Trying to compare two different records with same value.
        Book theSecretHistoryCopy = new ("\nThe secret history", "Donna Tartt", "1234567890");
        this._userInterface.ShowMessage(MessageType.Information, string.Format(
            Messages.IsEqual,
            nameof(theSecretHistory),
            nameof(theSecretHistoryCopy),
            theSecretHistory == theSecretHistoryCopy));

        // Trying to change the value of record.
        // Sapiens.Author = "Kalki"; // Will throw compile time error as record are immutable.
        Book saphiensAltered = sapiens with { author = "Kalki" };
        this._userInterface.ShowMessage(MessageType.Information, Messages.OriginalBook + sapiens);
        this._userInterface.ShowMessage(MessageType.Information, Messages.NewBook + saphiensAltered);

        // Using deconstruction to extract individual values of record.
        this._userInterface.ShowMessage(MessageType.Information, Messages.DisplayDetailsUsingDeconstruction);
        this.DisplayBook(theSecretHistory);

        this._userInterface.ShowMessage(MessageType.Prompt, string.Format(Messages.TaskExitPrompt, 6));
        this._userInterface.GetInput();
    }

    private void DisplayBook(Book book)
    {
        var (title, author, isbn) = book;

        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.BookDetails, title, author, isbn));
    }
}
