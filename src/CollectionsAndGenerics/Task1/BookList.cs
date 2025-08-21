using System.Collections;

namespace CollectionsAndGenerics.Task1;

/// <summary>
/// Represents the collection of book titles.
/// </summary>
/// <typeparam name="T">Specifies the type of book title.</typeparam>
public class BookList<T> : IEnumerable<T>, IEnumerable
{
    private List<T> _bookTitles = new ();

    /// <summary>
    /// Adds a new book title.
    /// </summary>
    /// <param name="name">Title of the book to add.</param>
    public void Add(T name) => this._bookTitles.Add(name);

    /// <summary>
    /// Removes the specified book title.
    /// </summary>
    /// <param name="name">Title of the book to remove.</param>
    public void Remove(T name) => this._bookTitles.Remove(name);

    /// <summary>
    /// Determines whether the specified book title exists in the book list.
    /// </summary>
    /// <param name="name">Title of the book name to determine.</param>
    /// <returns><see cref="true"/> if the book title exists; otherwise <see cref="false"/></returns>
    public bool Contains(T name) => this._bookTitles.Contains(name);

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator() => this._bookTitles.GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => this._bookTitles.GetEnumerator();
}
