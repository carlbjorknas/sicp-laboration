namespace SICP;

public class REPL
{
    private readonly IReader _reader;
    private readonly IPrinter _printer;
    private readonly Lexer _lexer;
    private readonly Parser _parser;
    private readonly Evaluator _evaluator;

    public REPL(IReader reader, IPrinter printer, Lexer lexer, Parser parser, Evaluator evaluator)
    {
        _reader = reader;
        _printer = printer;
        _lexer = lexer;
        _parser = parser;
        _evaluator = evaluator;
    }

    public void Run()
    {
        var env = new Environment();
        var command = _reader.Read();
        while(command != string.Empty)
        {            
            var tokens = _lexer.Tokenize(command);
            var expression = _parser.Parse(tokens);
            var evaluatedExpression = _evaluator.Eval(expression, env);
            _printer.Print(evaluatedExpression.ToString());

            command = _reader.Read();
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