using SICP.Exceptions;

namespace SICP;

public class REPL
{
    private readonly IPrinter _printer;
    private readonly Parser _parser;
    private readonly Evaluator _evaluator;

    public REPL(IPrinter printer, Parser parser, Evaluator evaluator)
    {
        _printer = printer;
        _parser = parser;
        _evaluator = evaluator;
    }

    public Environment Run()
    {
        var env = new Environment();
        while(true)
        {
            try
            {
                var expression = _parser.GetNextExpression();
                var evaluatedExpression = _evaluator.Eval(expression, env);
                _printer.Print(evaluatedExpression.ToString());
            }
            catch (QuitException)
            {
                return env;
            }
            catch (Exception ex)
            {
                _printer.Print(ex.Message);
            }
        }
    }
}

public interface IPrinter
{
    void Print(string v);
}

public interface IReader
{
    string Read();
}