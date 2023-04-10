using SICP.EvalResults;
using SICP.Exceptions;
using SICP.PrimitiveProcedures;

namespace SICP;

public class Environment
{
    private Dictionary<string, EvalResult> _varToValueMap = new();

    public Environment()
    {
        _varToValueMap.Add("+", new Plus());
        _varToValueMap.Add("-", new Minus());
    }

    public void AddVariable(string name, EvalResult value)
    {
        _varToValueMap.Add(name, value);
    }

    public EvalResult GetValue(string name)
    {
        if (_varToValueMap.TryGetValue(name, out var value))
            return value;

        throw new UnboundVariableException(name);
    }
}
