using SICP.Exceptions;
using SICP.Expressions;

namespace SICP;

public class Environment
{
    private readonly Dictionary<string, Expression> _varToValueMap = new();
    private readonly Environment? _enclosingEnvironment;

    public Environment()
    {
        AddVariable("+", new PrimitiveProcedurePlus());
        AddVariable("-", new PrimitiveProcedureMinus());
        AddVariable("*", new PrimitiveProcedureMultiplication());
        AddVariable("not", new PrimitiveProcedureNot());
        AddVariable("<", new PrimitiveProcedureLt());
        AddVariable("<=", new PrimitiveProcedureLte());
        AddVariable("=", new PrimitiveProcedureEquals());
        AddVariable(">=", new PrimitiveProcedureGte());
        AddVariable(">", new PrimitiveProcedureGt());
        AddVariable("eval", new PrimitiveProcedureEval());
        AddVariable("cons", new PrimitiveProcedureCons());
        AddVariable("car", new PrimitiveProcedureCar());
        AddVariable("cdr", new PrimitiveProcedureCdr());
        AddVariable("list", new PrimitiveProcedureList());
        AddVariable("append", new PrimitiveProcedureAppend());
        AddVariable("string?", new PrimitiveProcedureStringTest());
        AddVariable(PrimitiveProcedureStringLength.Name, new PrimitiveProcedureStringLength());
        AddVariable("quit", new PrimitiveProcedureQuit());
    }

    public Environment(List<string> parameters, List<Expression> arguments, Environment enclosingEnvironment)
    {
        _enclosingEnvironment = enclosingEnvironment;

        if (arguments.Count < parameters.Count)
            throw new InvalidNumberOfArgumentsException("Too few arguments supplied.", parameters, arguments);
        if (arguments.Count > parameters.Count)
            throw new InvalidNumberOfArgumentsException("Too many arguments supplied.", parameters, arguments);

        for (var i = 0; i < parameters.Count; i++)
        {
            AddVariable(parameters[i], arguments[i]);
        }
    }

    public void AddVariable(string name, Expression value)
    {
        _varToValueMap.Add(name, value);
    }

    public Expression GetValue(string name)
    {
        if (_varToValueMap.TryGetValue(name, out var value))
            return value;

        return _enclosingEnvironment != null 
            ? _enclosingEnvironment.GetValue(name)
            : throw new UnboundVariableException(name);
    }
}
