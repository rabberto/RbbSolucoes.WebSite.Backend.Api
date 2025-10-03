using System;
using System.Diagnostics.CodeAnalysis;
using RbbSolucoes.Website.Backend.Api.CrossCutting.Exceptions;

namespace RbbSolucoes.Website.Backend.Api.CrossCutting.Models;

[ExcludeFromCodeCoverage]
public class ErrorModel
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }

    public List<ErrorDetails> Erros { get; } = [];

    public ErrorModel(ErrorDetails erro)
    {
        Erros.Add(erro);
    }

    public ErrorModel(IEnumerable<ErrorDetails> erros)
    {
        Erros.AddRange(erros);
    }

    public ErrorModel(ValidationErrorException validationErrorException)
    {
        Erros.AddRange(validationErrorException.Errors.Select(x => new ErrorDetails
        {
            StatusCode = 400,
            Message = x
        }));
    }

    public ErrorModel(int statusCode, string message)
    {
        Erros.Add(new ErrorDetails
        {
            StatusCode = statusCode,
            Message = message
        });
    }
}
