//using SICP.EvalResults;

//namespace SICP.PrimitiveProcedures;

//internal class Minus : PrimitiveProcedureEvalResult
//{
//    public override EvalResult Apply(Evaluator engine, List<string> operands, Environment env)
//    {
//        var evaluatedOperands = operands.Select(operand => engine.Eval(operand, env)).ToList();

//        if (!operands.Any())
//            return new IntEvalResult(0);

//        if (operands.Count == 1)
//            return new IntEvalResult(-((IntEvalResult)evaluatedOperands[0]).Value);

//        var sum = evaluatedOperands.Skip(1).Cast<IntEvalResult>().Sum(x => x.Value);
//        return new IntEvalResult(((IntEvalResult)evaluatedOperands[0]).Value - sum);
//    }

//    public override string ToString() => "-";
//}
