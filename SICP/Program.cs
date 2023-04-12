// See https://aka.ms/new-console-template for more information
using SICP;

Console.WriteLine("Start coding!");
Console.WriteLine("Quit by entering 'q'.");

var evaluator = new Evaluator();
var environment = new SICP.Environment();
var code = Console.ReadLine();

// TODO Change to ctrl-c?
while(code is not ("q" or null))
{
    try
    {        
        var result = evaluator.Eval(code, environment);
        Console.WriteLine(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    } 
    
    code = Console.ReadLine();
}


