namespace LINQ.Tasks.Task5;

/// <summary>
/// allows users to construct complex LINQ queries using a fluent API pattern.
/// </summary>
/// <typeparam name="T1">Type of first object to query.</typeparam>
/// <typeparam name="T2">Type of second object to query.</typeparam>
public class QueryBuilder<T1, T2>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QueryBuilder{T1, T2}"/> class.
    /// </summary>
    /// <param name="input1"><see cref="IEnumerable"/> 1 to query.</param>
    /// <param name="input2"><see cref="IEnumerable"/> 2 to query.</param>
    public QueryBuilder(IEnumerable<T1> input1, IEnumerable<T2> input2)
    {
        this.Enumberable1 = input1;
        this.Enumberable2 = input2;
    }

    private Func<T1, bool>? FilterPredicate { get; set; }

    private Func<T1, object>? Property { get; set; }

    private Func<T1, T2, bool>? JoinCondition { get; set; }

    private IEnumerable<T1> Enumberable1 { get; set; }

    private IEnumerable<T2> Enumberable2 { get; set; }

    /// <summary>
    /// Accepts a lambda expression that represents a filter condition and adds it to the query.
    /// </summary>
    /// <param name="filterPredicate">Filter condition.</param>
    /// <returns>Object of <see cref="QueryBuilder{T1, T2}"/></returns>
    public QueryBuilder<T1, T2> Filter(Func<T1, bool> filterPredicate)
    {
        this.FilterPredicate = filterPredicate;
        return this;
    }

    /// <summary>
    /// Accepts a lambda expression that represents the property to sort by and adds it to the query.
    /// </summary>
    /// <param name="property">Sort property.</param>
    /// <returns>Object of <see cref="QueryBuilder{T1, T2}"/></returns>
    public QueryBuilder<T1, T2> SortBy(Func<T1, object> property)
    {
        this.Property = property;
        return this;
    }

    /// <summary>
    /// Accepts a lambda expression that represents the property to sort by and adds it to the query.
    /// </summary>
    /// <param name="join">Sort property.</param>
    /// <returns>Object of <see cref="QueryBuilder{T1, T2}"/></returns>
    public QueryBuilder<T1, T2> Join(Func<T1, T2, bool> join)
    {
        this.JoinCondition = join;
        return this;
    }

    /// <summary>
    ///  Executes the constructed LINQ query and returns the result.
    /// </summary>
    /// <returns>Result</returns>
    public IEnumerable<object> Execute()
    {
        List<T1> result = new List<T1>();
        if (this.FilterPredicate is not null)
        {
            foreach (var item in this.Enumberable1)
            {
                if (this.FilterPredicate(item))
                {
                    result.Add(item);
                }
            }
        }

        if (this.Property is not null)
        {
            result = result.OrderBy(this.Property).ToList();
        }

        if (this.JoinCondition is null)
        {
            return (IEnumerable<object>)result;
        }

        List<object> joinedResult = new List<object>();
        foreach (T1 t1 in result)
        {
            foreach (T2 t2 in this.Enumberable2)
            {
                if (this.JoinCondition(t1, t2))
                {
                    joinedResult.Add(new Tuple<T1, T2>(t1, t2));
                }
            }
        }

        return (IEnumerable<object>)joinedResult;
    }
}