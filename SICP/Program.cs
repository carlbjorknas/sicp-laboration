// See https://aka.ms/new-console-template for more information
using SICP;

Console.WriteLine("Start coding!");
Console.WriteLine("Quit by entering the command '(quit)'.");

var reader = new Reader();
var lexer = new Lexer(reader);
var parser = new Parser(lexer);

var repl = new REPL(new Printer(), parser, new Evaluator());
repl.Run();


