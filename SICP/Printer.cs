// See https://aka.ms/new-console-template for more information
using SICP;

class Printer : IPrinter
{
    public void Print(string text)
    {
        Console.WriteLine("--> " + text);
        Console.WriteLine();
    }
}


