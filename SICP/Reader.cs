// See https://aka.ms/new-console-template for more information
using SICP;

class Reader : IReader
{
    public string Read()
    {
        return Console.ReadLine()!;
    }
}