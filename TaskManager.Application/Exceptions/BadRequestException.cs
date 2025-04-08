using FluentValidation.Results;

namespace TaskManager.Application.Exceptions;

public class BadRequestException : Exception
{
    public List<string> ValidationErrors { get; set; } = new List<string>();

    public BadRequestException(string message):base(message)
    { }

    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
    }
}
