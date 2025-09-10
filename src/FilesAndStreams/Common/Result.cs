namespace FilesAndStreams.Common;

/// <summary>
/// Represents the outcome of operation indicating success with result or failure with error message.
/// </summary>
/// /// <typeparam name="T">Type of variable returned in case of success</typeparam>
public class Result<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    private Result(bool isSuccess, T? value, string? errorMessage)
    {
        this.IsSuccess = isSuccess;
#pragma warning disable CS8601 // Possible null reference assignment.
        this.Value = value;
        this.ErrorMessage = errorMessage;
#pragma warning restore CS8601 // Possible null reference assignment.
    }

    /// <summary>
    /// Gets a value indicating whether the operation is success or failure.
    /// </summary>
    /// <value>
    /// A value indicating whether the operation is success or failure.
    /// </value>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets the value of the result if operation is successful.
    /// </summary>
    /// <value>
    /// The value of the result if operation is successful.
    /// </value>
    public T Value { get; }

    /// <summary>
    /// Gets the error message specifying what error happened when the operation failed.
    /// </summary>
    /// <value>
    /// The error message specifying what error happened when the operation failed.
    /// </value>
    public string ErrorMessage { get; }

    /// <summary>
    /// Create a successful object of <see cref="Result{T}"/> with the result.
    /// </summary>
    /// <param name="value">Value of result.</param>
    /// <returns>A <see cref="Result{T}"/> representing success.</returns>
    public static Result<T> Success(T value) => new (true, value, null);

    /// <summary>
    /// Create a failure object of <see cref="Result{T}"/> with the error message.
    /// </summary>
    /// <param name="errorMessage">Error message describing the failure.</param>
    /// <returns>A <see cref="Result{T}"/> representing failure.</returns>
    public static Result<T> Failure(string errorMessage) => new (false, default, errorMessage);
}
