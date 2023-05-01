using SICP.Exceptions;

namespace SICP;

public class Environment
{
    private readonly Dictionary<string, Expression> _varToValueMap = new();

    public Environment()
    {
        AddVariable("+", PrimitiveProcedurePlus.Instance);
        AddVariable("-", PrimitiveProcedureMinus.Instance);
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
