namespace Reflections.Common;

/// <summary>
/// Represents the outcome of operations whether indicating success with result value
/// or failure with error message.
/// </summary>
/// <typeparam name="T">Type of variable returned in case of success</typeparam>
public class Result<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    private Result(bool success, T value, string errorMessage)
    {
        this.IsSuccess = success;
        this.Value = value;
        this.ErrorMessage = errorMessage;
    }

    /// <summary>
    /// Gets a value indicating whether the operation is successful or not.
    /// </summary>
    /// <value>
    /// A value indicating whether the operation is successful or not.
    /// </value>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value of the result if operation is successful.
    /// </summary>
    /// <value>
    /// A value of the result if operation is successful.
    /// </value>
    public T Value { get; }

    /// <summary>
    /// Gets the error message if operation failed.
    /// </summary>
    /// <value>
    /// The error message if operation failed.
    /// </value>
    public string ErrorMessage { get; }

    /// <summary>
    /// Create a successful object of <see cref="Result{T}"/> with the result.
    /// </summary>
    /// <param name="value">Value of result.</param>
    /// <returns>A <see cref="Result{T}"/> representing success.</returns>
    public static Result<T> Success(T value) => new Result<T>(true, value, string.Empty);

    /// <summary>
    /// Create a failure object of <see cref="Result{T}"/> with the error message.
    /// </summary>
    /// <param name="errorMessage">Error message describing the failure.</param>
    /// <returns>A <see cref="Result{T}"/> representing failure.</returns>
    public static Result<T> Failure(string errorMessage) => new Result<T>(false, default!, errorMessage);
}
