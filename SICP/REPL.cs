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
        var command = _reader.Read();
        while(command != string.Empty)
        {
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