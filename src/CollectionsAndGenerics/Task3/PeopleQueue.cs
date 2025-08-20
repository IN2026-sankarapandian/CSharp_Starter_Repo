using System.Collections;

namespace CollectionsAndGenerics.Task3;

/// <summary>
/// Represents the FIFO queue of peoples.
/// </summary>
/// <typeparam name="T">Specifies the type of people in the queue.</typeparam>
public class PeopleQueue<T> : IEnumerable<T>, IEnumerable
{
    private Queue<T> _peoples = new ();

    /// <summary>
    /// Enqueue the specified people to queue.
    /// </summary>
    /// <param name="item">People to enqueue.</param>
    public void Enqueue(T item)
    {
        this._peoples.Enqueue(item);
    }

    /// <summary>
    /// Dequeue the people at queue.
    /// </summary>
    /// <returns>Dequeued people.</returns>
    public T Dequeue() => this._peoples.Dequeue();

    /// <summary>
    /// Determines whether custom queue contain any peoples.
    /// </summary>
    /// <returns><see cref="true"/> if it has peoples; otherwise <see cref="false"/></returns>
    public bool HasPeople() => this._peoples.Count > 0;

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator() => this._peoples.GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => this._peoples.GetEnumerator();
}
