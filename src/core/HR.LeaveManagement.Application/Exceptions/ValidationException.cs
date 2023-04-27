using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions;

public class ValidationException: ApplicationException
{
    public List<string> Errors = new List<string>();

    public ValidationException(ValidationResult result)
    {
        foreach(var error in result.Errors)
        {
            Errors.Add(error.ErrorMessage);
        }
    }
}
