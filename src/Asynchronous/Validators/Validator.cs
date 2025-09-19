using Asynchronous.Common;
using Asynchronous.Constants;

namespace Asynchronous.Validators;

/// <summary>
/// It provide methods to validate data required by this assignment from users.
/// </summary>
public class Validator
{
    /// <summary>
    /// Validate the given string is a valid URL or not.
    /// </summary>
    /// <param name="url">URL string to validate.</param>
    /// <returns>It returns the <see cref="Result{bool}"/> success object the given string is valid url with true;
    /// other wise the failure object with the error message.</returns>
    public Result<bool> ValidateURL(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult))
        {
            return Result<bool>.Failure(Messages.NotValidURL);
        }

        return Result<bool>.Success(true);
    }

    /// <summary>
    /// Validates whether it is a valid path of a text file.
    /// </summary>
    /// <param name="path">Path of the file to validate.</param>
    /// <returns>Returns the <see cref="Result{T}"/> with value true if it is valid path: otherwise false with the error message.</returns>
    public Result<bool> IsValidTextFilePath(string path)
    {
        if (Path.GetInvalidPathChars().Any(invalidChar => path.Contains(invalidChar)))
        {
            return Result<bool>.Failure(Messages.InvalidCharacters);
        }
        else if (!Path.IsPathRooted(path))
        {
            return Result<bool>.Failure(Messages.NotAbsolutePath);
        }
        else if (Path.GetExtension(path) != ".txt")
        {
            return Result<bool>.Failure(Messages.NotTxtFile);
        }
        else if (!File.Exists(path))
        {
            return Result<bool>.Failure(Messages.NoFileExists);
        }

        return Result<bool>.Success(true);
    }
}
