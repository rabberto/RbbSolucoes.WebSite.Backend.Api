using System;
using System.Diagnostics.CodeAnalysis;

namespace RbbSolucoes.Website.Backend.Api.CrossCutting.Exceptions;

[ExcludeFromCodeCoverage]
public class ValidationErrorException : Exception
{
    public readonly IEnumerable<string> Errors;

    public ValidationErrorException(string message) : base(message)
    {
        Errors = [message];
    }

    public ValidationErrorException(IEnumerable<string> errors) : base("Validation errors occurred")
    {
        Errors = errors;
    }
}
