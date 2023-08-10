using SICP.Expressions;

namespace SICP.Exceptions;

internal class InvalidNumberOfArgumentsException : Exception
{
    public InvalidNumberOfArgumentsException(string message, List<string> parameters, List<Expression> arguments) : base(message)
    {
        Parameters = parameters;
        Arguments = arguments;
    }

    public List<string> Parameters { get; }
    public List<Expression> Arguments { get; }
}
