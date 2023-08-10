namespace SICP.Expressions;

internal class CompoundProcedure : Expression
{
    private readonly List<string> _parameters;
    private readonly Expression _body;

    public CompoundProcedure(List<string> parameters, Expression body)
    {
        _parameters = parameters;
        _body = body;
    }

    public override string ToString()
    {
        return $"(lambda ({string.Join(" ", _parameters)}) {_body})";
    }
}
