using SICP.ExtensionMethods;

namespace SICP.Expressions;

internal class PrimitiveProcedureHelp : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands, Environment callerEnvironment)
    {
        EnsureOperandsHaveExpectedCount(operands, 0, "help");

        var names = callerEnvironment.GetVariableNames()
            .Concat(Evaluator.SpecialFormNames)
            .Distinct()
            .OrderBy(name => name, StringComparer.Ordinal);

        return names
            .Select(name => (Expression)new VariableExpression(name))
            .ToPairs();
    }
}
