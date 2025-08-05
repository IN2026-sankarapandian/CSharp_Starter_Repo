namespace LINQ.Tasks.Task5;

/// <summary>
/// allows users to construct complex LINQ queries using a fluent API pattern.
/// </summary>
/// <typeparam name="T1">Type of first object to query.</typeparam>
public class QueryBuilder<T1>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QueryBuilder{T1}"/> class.
    /// </summary>
    /// <param name="enumerable1"><see cref="IEnumerable"/> 1 to query.</param>
    /// <param name="input2"><see cref="IEnumerable"/> 2 to query.</param>
    public QueryBuilder(IEnumerable<T1> enumerable1)
    {
        this.Enu1 = enumerable1;
    }

    /// <summary>
    /// Gets or sets input enumerable.
    /// </summary>
    /// <value>
    /// Input enumerable.
    /// </value>
    protected IEnumerable<T1> Enu1 { get; set; }

    /// <summary>
    /// Gets or sets predicate function to filter.
    /// </summary>
    /// <value>
    /// Predicate function to filter.
    /// </value>
    protected Func<T1, bool>? Predicate { get;  set; }

    /// <summary>
    /// Gets or sets key selector to sort.
    /// </summary>
    /// <value>
    /// Key selector to sort.
    /// </value>
    protected Func<T1, object>? KeySelector { get; set; }

    /// <summary>
    /// Accepts a lambda expression that represents a filter condition and adds it to the query.
    /// </summary>
    /// <param name="predicate">Filter condition.</param>
    /// <returns>Object of <see cref="QueryBuilder{T1}"/></returns>
    public QueryBuilder<T1> Filter(Func<T1, bool> predicate)
    {
        this.Predicate = predicate;
        return this;
    }

    /// <summary>
    /// Add key selector
    /// </summary>
    /// <param name="keySelector">Key selector.</param>
    /// <returns>Object of <see cref="QueryBuilder{T1}"/></returns>
    public QueryBuilder<T1> SortBy(Func<T1, object> keySelector)
    {
        this.KeySelector = keySelector;
        return this;
    }

    /// <summary>
    /// Joins the two enumerable..
    /// </summary>
    /// <typeparam name="TResult">Type of result.</typeparam>
    /// <typeparam name="T2">Type of outer</typeparam>
    /// <param name="outer">Enumerable to join.</param>
    /// <param name="joinCondition">Join condition</param>
    /// <param name="resultSelector">Final object</param>
    /// <returns>Joined result.</returns>
    public QueryBuilder<T1, T2, TResult> Join<TResult, T2>(IEnumerable<T2> outer, Func<T1, T2, bool> joinCondition, Func<T1, T2, TResult> resultSelector)
    {
        return new QueryBuilder<T1, T2, TResult>(this.Enu1, outer, joinCondition, resultSelector, this.Predicate, this.KeySelector);
    }

    /// <summary>
    /// Executes the constructed LINQ query and returns the result.
    /// </summary>
    /// <returns>Result</returns>
    public IEnumerable<T1> Execute()
    {
        List<T1> enumerableUpdated = new List<T1>();
        if (this.Predicate is not null)
        {
            foreach (var item in this.Enu1)
            {
                if (this.Predicate(item))
                {
                    enumerableUpdated.Add(item);
                }
            }
        }

        if (this.KeySelector is not null)
        {
            enumerableUpdated = enumerableUpdated.OrderBy(this.KeySelector).ToList();
        }

        return enumerableUpdated;
    }
}

/// <summary>
/// allows users to construct complex LINQ queries using a fluent API pattern.
/// </summary>
/// <typeparam name="T1">Type of first object to query.</typeparam>
/// <typeparam name="TInner">Type of inner object.</typeparam>
/// <typeparam name="TResult">Type of result object.</typeparam>
public class QueryBuilder<T1, TInner, TResult> : QueryBuilder<T1>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QueryBuilder{T1, TInner, TResult}"/> class.
    /// </summary>
    /// <param name="outer">Outer object.</param>
    /// <param name="inner">Inner object.</param>
    /// <param name="joinCondition">Join condition.</param>
    /// <param name="resultSelector">Result object</param>
    /// <param name="filterPredicate">Filter conditions.</param>
    /// <param name="keySelector">Key selector for sort.</param>
    public QueryBuilder(IEnumerable<T1> outer, IEnumerable<TInner> inner, Func<T1, TInner, bool>? joinCondition, Func<T1, TInner, TResult>? resultSelector, Func<T1, bool>? filterPredicate, Func<T1, object>? keySelector)
        : base(outer)
    {
        this.Enu1 = outer;
        this.Inner = inner;
        this.JoinCondition = joinCondition;
        this.ResultSelector = resultSelector;
        this.Predicate = filterPredicate;
        this.KeySelector = keySelector;
    }

    /// <summary>
    /// Gets or sets inner object to join.
    /// </summary>
    /// <value>
    /// Inner object to join.
    /// </value>
    protected IEnumerable<TInner> Inner { get; set; }

    /// <summary>
    /// Gets or sets condition to Join.
    /// </summary>
    /// <value>
    /// Condition to Join.
    /// </value>
    protected Func<T1, TInner, bool>? JoinCondition { get; set; }

    /// <summary>
    /// Gets or sets result object after join.
    /// </summary>
    /// <value>
    /// Result object after join.
    /// </value>
    protected Func<T1, TInner, TResult>? ResultSelector { get; set; }

    /// <summary>
    /// Joins the two enumerable..
    /// </summary>
    /// <param name="joinCondition">Join condition</param>
    /// <param name="resultSelector">Final object</param>
    /// <returns>Joined result.</returns>
    public QueryBuilder<T1, TInner, TResult> Join(Func<T1, TInner, bool> joinCondition, Func<T1, TInner, TResult> resultSelector)
    {
        this.JoinCondition = joinCondition;
        this.ResultSelector = resultSelector;
        return this;
    }

    /// <summary>
    /// Executes the constructed LINQ query and returns the result.
    /// </summary>
    /// <returns>Result</returns>
    public new IEnumerable<TResult> Execute()
    {
        IEnumerable<T1> result = base.Execute();
        List<TResult> joinedResult = new List<TResult>();

        foreach (var e1 in result)
        {
            foreach (var e2 in this.Inner)
            {
                if (this.JoinCondition is not null && this.ResultSelector is not null && this.JoinCondition(e1, e2))
                {
                    joinedResult.Add(this.ResultSelector(e1, e2));
                }
            }
        }

        return joinedResult;
    }
}