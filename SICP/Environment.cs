using SICP.Exceptions;
using SICP.Expressions;

namespace SICP;

public class Environment
{
    private readonly Dictionary<string, Expression> _varToValueMap = new();
    private readonly Environment? _enclosingEnvironment;

    public Environment() : this(null)
    {
        AddVariable("+", new PrimitiveProcedurePlus());
        AddVariable("-", new PrimitiveProcedureMinus());
        AddVariable("*", new PrimitiveProcedureMultiplication());
        AddVariable("not", new PrimitiveProcedureNot());
        AddVariable("<", new PrimitiveProcedureLt());
        AddVariable("<=", new PrimitiveProcedureLte());
        AddVariable(">=", new PrimitiveProcedureGte());
        AddVariable(">", new PrimitiveProcedureGt());
        AddVariable("eval", new PrimitiveProcedureEval());
        AddVariable("cons", new PrimitiveProcedureCons());
        AddVariable("car", new PrimitiveProcedureCar());
        AddVariable("cdr", new PrimitiveProcedureCdr());
        AddVariable("list", new PrimitiveProcedureList());
        AddVariable("append", new PrimitiveProcedureAppend());
    }

    public Environment(Environment? enclosingEnvironment)
    {
        _enclosingEnvironment = enclosingEnvironment;
    }

    public void AddVariable(string name, Expression value)
    {
        _varToValueMap.Add(name, value);
    }

    public Expression GetValue(string name)
    {
        if (_varToValueMap.TryGetValue(name, out var value))
            return value;

        throw new UnboundVariableException(name);
    }

    internal Environment ExtendWith(List<string> parameters, List<Expression> arguments)
    {
        if (arguments.Count < parameters.Count)
            throw new InvalidNumberOfArgumentsException("Too few arguments supplied.", parameters, arguments);
        if (arguments.Count > parameters.Count)
            throw new InvalidNumberOfArgumentsException("Too many arguments supplied.", parameters, arguments);

        var extendedEnvironment = new Environment(enclosingEnvironment: this);

        for (var i = 0 ; i < parameters.Count; i++)
        {
            extendedEnvironment.AddVariable(parameters[i], arguments[i]);
        }

        return extendedEnvironment;
    }
}
