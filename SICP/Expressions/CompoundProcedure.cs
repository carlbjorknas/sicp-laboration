namespace SICP.Expressions;

internal class CompoundProcedure : Expression
{
    private readonly List<string> _parameters;
    private readonly Expression _body;
    private readonly Environment _environment;

    public CompoundProcedure(List<string> parameters, Expression body, Environment environment)
    {
        _parameters = parameters;
        _body = body;
        _environment = environment;
    }

    public Expression Apply(List<Expression> arguments)
    {
        var extendedEnvironment = new Environment(_parameters, arguments, enclosingEnvironment: _environment);
        return new Evaluator().Eval(_body, extendedEnvironment);
    }

    public override string ToString()
    {
        return $"(lambda ({string.Join(" ", _parameters)}) {_body})";
    }
}
