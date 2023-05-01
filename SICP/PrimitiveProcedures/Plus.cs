//using SICP.EvalResults;

//namespace SICP.PrimitiveProcedures;

//internal class Plus : PrimitiveProcedureEvalResult
//{
//    public override EvalResult Apply(Evaluator engine, List<string> operands, Environment env)
//    {
//        var evaluatedOperands = operands
//            .Select(operand => engine.Eval(operand, env))
//            .ToList();

//        return new IntEvalResult(evaluatedOperands.Cast<IntEvalResult>().Sum(x => x.Value));
//    }

//    public override string ToString() => "+";
//}
