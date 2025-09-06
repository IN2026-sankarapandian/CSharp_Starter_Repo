using Microsoft.CodeAnalysis.CSharp;
using Reflections.Constants;
using Reflections.Handlers;

namespace Reflections.Validators;

/// <summary>
/// Provide validator methods for reflection based operations.
/// </summary>
public class Validator
{
    /// <summary>
    /// Validate whether the specified name is a valid identifier name.
    /// </summary>
    /// <param name="name">Identifier name to validate.</param>
    /// <returns><see cref="Result{bool}"/> object indicating success with true or failure with error message.</returns>
    public Result<bool> IsValidIdentifier(string name)
    {
        if (!SyntaxFacts.IsValidIdentifier(name))
        {
            return Result<bool>.Failure(Messages.NotValidIdentifierName);
        }
        else if (SyntaxFacts.GetKeywordKind(name) != SyntaxKind.None)
        {
            return Result<bool>.Failure(string.Format(Messages.NameIsReservedKeyword, name));
        }

        return Result<bool>.Success(true);
    }
}
