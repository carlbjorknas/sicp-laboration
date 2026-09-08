using SICP.Expressions;

namespace SICP.SpecialForms;

internal static class SpecialFormLambda
{
    public const string Tag = "lambda";

    internal static bool Recognises(Expression expression) => expression.IsTaggedList(Tag);

    static List<string> Parameters(PairExpression list)
        => ((PairExpression)list.Cadr).ToDotNetList().Cast<VariableExpression>().Select(x => x.Value).ToList();

    static Expression Body(PairExpression list) => list.Caddr;

    public static Expression MakeProcedure(Expression expression, Environment env)
    {
        var list = (PairExpression)expression;
        return new CompoundProcedure(Parameters(list), Body(list), env);
    }
}
