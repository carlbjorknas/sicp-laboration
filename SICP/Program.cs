// See https://aka.ms/new-console-template for more information
using SICP;

Console.WriteLine("Start coding!");
Console.WriteLine("Quit by entering 'q'.");

var engine = new Engine();
var code = Console.ReadLine();

// TODO Change to ctrl-c?
while(code != "q")
{
    try
    {
        var result = engine.Eval(code);
        Console.WriteLine(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    } 
    
    code = Console.ReadLine();
}


