namespace CreatorOS.Shared.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.") { }
}

public class ForbiddenException : Exception
{
    public ForbiddenException(string message = "Access denied.")
        : base(message) { }
}

public class ValidationException : Exception
{
    public IEnumerable<string> Errors { get; }

    public ValidationException(IEnumerable<string> errors)
        : base("One or more validation failures occurred.")
    {
        Errors = errors;
    }
}
