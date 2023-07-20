using SICP.Exceptions;
using SICP.Expressions;

namespace SICP;

public class Environment
{
    private readonly Dictionary<string, Expression> _varToValueMap = new();

    public Environment()
    {
        AddVariable("+", new PrimitiveProcedurePlus());
        AddVariable("-", new PrimitiveProcedureMinus());
        AddVariable("not", new PrimitiveProcedureNot());
        AddVariable("<", new PrimitiveProcedureLt());
        AddVariable("<=", new PrimitiveProcedureLte());
        AddVariable(">=", new PrimitiveProcedureGte());
        AddVariable(">", new PrimitiveProcedureGt());
        AddVariable("eval", new PrimitiveProcedureEval());
        AddVariable("cons", new PrimitiveProcedureCons());
        AddVariable("car", new PrimitiveProcedureCar());
        AddVariable("list", new PrimitiveProcedureList());
        AddVariable("append", new PrimitiveProcedureAppend());
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
}
