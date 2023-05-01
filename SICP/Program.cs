// See https://aka.ms/new-console-template for more information
using SICP;

Console.WriteLine("Start coding!");
Console.WriteLine("Quit by entering an empty line.");

var repl = new REPL(new Reader(), new Printer(), new Lexer(), new Parser(), new Evaluator());
repl.Run();

class Reader : IReader
{
    public string Read() => Console.ReadLine();
}

class Printer : IPrinter
{
    public void Print(string text) => Console.WriteLine(text);
}


