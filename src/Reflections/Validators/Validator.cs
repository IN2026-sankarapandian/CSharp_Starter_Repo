using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using Reflections.Handlers;

namespace Reflections.Validators;

/// <summary>
/// Provide validator methods for dynamic assembly.
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
            return Result<bool>.Failure("Not a valid identifier name");
        }
        else if (SyntaxFacts.GetKeywordKind(name) != SyntaxKind.None)
        {
            return Result<bool>.Failure($"{name} is a reserved keyword in c#");
        }

        return Result<bool>.Success(true);
    }

    /// <summary>
    /// Validate a method whether it executed by app or not
    /// </summary>
    /// <param name="methodInfo">Method info of the method to validate with.</param>
    /// <returns><see cref="true"/> id it can be executed; otherwise <see cref="false"/></returns>
    public bool IsSupportedMethod(MethodInfo methodInfo)
    {
        ParameterInfo[] propertyInfos = methodInfo.GetParameters();
        return propertyInfos.All(p =>
            p.ParameterType.IsPrimitive ||
            p.ParameterType == typeof(decimal) ||
            p.ParameterType == typeof(string));
    }

    public Result<bool> IsSupportedParameter(PropertyInfo propertyInfo)
    {
        if (!propertyInfo.CanWrite)
        {
            Result<bool>.Failure("Property restricts write");
        }
        else if (!(propertyInfo.GetType() == typeof(string) ||
            propertyInfo.GetType() == typeof(decimal) ||
            propertyInfo.GetType().IsPrimitive))
        {
            Result<bool>.Failure("Property type not supported");
        }

        return Result<bool>.Success(true);
    }
}
