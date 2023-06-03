using SICP.Expressions;

namespace SICP.SpecialForms;

internal static class SpecialFormAssignment
{
    public static bool Recognises(Expression expr) => expr.IsTaggedList("define");
    static string AssignmentVariable(ListExpression list) => ((VariableExpression)list.Cadr).Value;
    static Expression AssignmentValue(ListExpression list) => list.Caddr;
    public static Expression Evaluate(Expression expr, Evaluator evaluator, Environment env)
    {
        var list = (ListExpression)expr;
        var name = AssignmentVariable(list);
        var evaluatedValue = evaluator.Eval(AssignmentValue(list), env);
        env.AddVariable(name, evaluatedValue);
        return new VariableExpression("ok");
    }
}
