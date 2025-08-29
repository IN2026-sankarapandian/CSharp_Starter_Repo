namespace CollectionsAndGenerics.Task2;

/// <summary>
/// Represents the LIFO stack of elements.
/// </summary>
/// <typeparam name="T">Specifies the type of elements in the stack.</typeparam>
public class CustomStack<T>
{
    private readonly Stack<T> _elements = new ();

    /// <summary>
    /// Push an element to the stack.
    /// </summary>
    /// <param name="item">Element to push.</param>
    public void Push(T item) => this._elements.Push(item);

    /// <summary>
    /// Pop an element from the stack.
    /// </summary>
    /// <returns>Popped element.</returns>
    public T Pop() => this._elements.Pop();

    /// <summary>
    /// Determines whether stack contain any elements.
    /// </summary>
    /// <returns><see cref="true"/> if it has elements; otherwise <see cref="false"/></returns>
    public bool HasElement() => this._elements.Count > 0;
}
