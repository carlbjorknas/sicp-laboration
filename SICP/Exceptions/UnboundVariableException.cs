namespace SICP.Exceptions;

public class UnboundVariableException : Exception
{
    public UnboundVariableException(string variable) : base($"Variable '{variable}' is unbound.")
    {
    }
}
