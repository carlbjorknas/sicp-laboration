using SICP.Expressions;

namespace SICP.SpecialForms;

internal static class SpecialFormAssignment
{
    public static bool Recognises(Expression expr) => expr.IsTaggedList("define");
     
    // The standard procedure definition follows the format below,
    // i e the second object is a list instead of a symbol.
    // (define (<var> <parameter_1> ... <parameter_n>) <body>)
    //
    // Compare with a value assigment:
    // (define <var> <value>)
    static bool IsStandardProcedureDefinition(PairExpression list) => list.Cadr is PairExpression;

    public static Expression Evaluate(Expression expr, Evaluator evaluator, Environment env)
    {
        var list = (PairExpression)expr;
        string name;
        Expression value;

        if (IsStandardProcedureDefinition(list))
        {
            name = ((VariableExpression)list.Caadr).Value;
            value = MakeLambda(list);
        }
        else
        {
            name = ((VariableExpression)list.Cadr).Value;
            value = list.Caddr;
        }

        var evaluatedValue = evaluator.Eval(value, env);
        env.AddVariable(name, evaluatedValue);

        return new VariableExpression("ok");
    }

    private static Expression MakeLambda(PairExpression list)
    {
        // Extract data from format
        // (define (<var> <parameter_1> ... <parameter_n>) <body>)
        // and use it to create a lambda having the format
        // (lambda (<parameter_1> ... <parameter_n>) <body>)
        
        var parameters = list.Cdadr;
        var body = list.Cddr;

        var paramsAndBody = new PairExpression(parameters, body);
        return new PairExpression(new VariableExpression("lambda"), paramsAndBody);
    }
}
