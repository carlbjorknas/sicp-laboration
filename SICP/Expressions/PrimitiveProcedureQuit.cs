using SICP.Exceptions;

namespace SICP.Expressions;

internal class PrimitiveProcedureQuit : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands)
    {
        throw new QuitException();
    }
}
