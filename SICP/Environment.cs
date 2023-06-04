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
        AddVariable(">", new PrimitiveProcedureGt());
        AddVariable("<", new PrimitiveProcedureLt());
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
