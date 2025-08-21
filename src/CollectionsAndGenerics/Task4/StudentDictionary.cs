using System.Collections;

namespace CollectionsAndGenerics.Task4;

/// <summary>
/// Represents the collection of student and their grades.
/// </summary>
/// <typeparam name="TKey">Specifies the type of student name.</typeparam>
/// <typeparam name="TValue">Specifies the type of student grade.</typeparam>
public class StudentDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
    where TKey : notnull
{
    /// <summary>
    /// Gets the dictionary of student name and grades.
    /// </summary>
    /// <value>Dictionary with student name and grades.</value>
    public Dictionary<TKey, TValue> KeyValuePairs { get; private set; } = new Dictionary<TKey, TValue>();

    /// <summary>
    /// Add a student data to the dictionary.
    /// </summary>
    /// <param name="name">Name of the student.</param>
    /// <param name="grade">Grade of the student.</param>
    public void Add(TKey name, TValue grade) => this.KeyValuePairs.Add(name, grade);

    /// <summary>
    /// Removes the specified student data.
    /// </summary>
    /// <param name="name">Name of the student to remove.</param>
    public void Remove(TKey name) => this.KeyValuePairs.Remove(name);

    /// <inheritdoc/>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this.KeyValuePairs.GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => this.KeyValuePairs.GetEnumerator();
}
