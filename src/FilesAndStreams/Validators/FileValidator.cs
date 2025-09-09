using System.IO;
using FilesAndStreams.Common;

namespace FilesAndStreams.Validators;

/// <summary>
/// Provide methods to validate file related data.
/// </summary>
public class FileValidator
{
    /// <summary>
    /// Validates whether it is a valid path to create a text file.
    /// </summary>
    /// <param name="path">Path of the file to validate.</param>
    /// <returns>Returns the <see cref="Result{T}"/> with value true if it is valid path: otherwise false with the error message.</returns>
    public Result<bool> IsValidTextFileSavePath(string path)
    {
        if (path.Any(c => Path.GetInvalidPathChars().Contains(c)))
        {
            return Result<bool>.Failure("Path contain invalid characters");
        }
        else if (!Path.IsPathRooted(path))
        {
            return Result<bool>.Failure("Path must be absolute. ex: C:/asd/asd.txt");
        }
        else if (Path.GetExtension(path) != ".txt")
        {
            return Result<bool>.Failure("Only .txt files are supported");
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
            return Result<bool>.Failure("Path contain invalid characters");
        }
        else if (!Path.IsPathRooted(path))
        {
            return Result<bool>.Failure("Path must be absolute. ex: C:/asd/asd.txt");
        }
        else if (Path.GetExtension(path) != ".txt")
        {
            return Result<bool>.Failure("Only .txt files are supported");
        }
        else if (!File.Exists(path))
        {
            return Result<bool>.Failure("No files exists in specified path");
        }

        return Result<bool>.Success(true);
    }
}
