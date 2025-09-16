namespace BoilerMachineApp.Common;

/// <summary>
/// Its an object implementing result pattern indicate success/ failure with respective messages.
/// </summary>
public class Result
{
    private Result(bool isSuccess, string? errorMessage, string? successMessage)
    {
        this.IsSuccess = isSuccess;
        this.ErrorMessage = errorMessage ?? string.Empty;
        this.SuccessMessage = successMessage ?? string.Empty;
    }

    /// <summary>
    /// Gets or sets a value indicating whether indicates whether the operation is success or not.
    /// </summary>
    /// <value>
    /// A value indicating whether indicates whether the operation is success or not.
    /// </value>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Gets or sets an error message in case of operation failed.
    /// </summary>
    /// <value>
    /// An error message in case of operation failed.
    /// </value>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets an success message in case of operation success.
    /// </summary>
    /// <value>
    /// An success message in case of operation success.
    /// </value>
    public string SuccessMessage { get; set; }

    /// <summary>
    /// Creates a success object of message.
    /// </summary>
    /// <param name="message">Success message</param>
    /// <returns>Result object indication success of operation.</returns>
    public static Result Success(string message) => new Result(true, default, message);

    /// <summary>
    /// Creates a failure object of message.
    /// </summary>
    /// <param name="message">Failure message</param>
    /// <returns>Result object indication failure of operation.</returns>
    public static Result Failure(string message) => new Result(false, message, default);
}
