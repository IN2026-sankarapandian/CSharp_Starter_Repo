namespace Reflections.Handlers;

public class Result<T>
{
    private Result(bool success, T? value, string? errorMessage)
    {
        this.IsSuccess = success;
        this.Value = value;
        this.ErrorMessage = errorMessage;
    }

    public bool IsSuccess { get; }
    public T Value { get; }
    public string ErrorMessage { get; }

    public static Result<T> Success(T value) => new Result<T>(true, value, null);
    public static Result<T> Failure(string error) => new Result<T>(false, default, error);
}
