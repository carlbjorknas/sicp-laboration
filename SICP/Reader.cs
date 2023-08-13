// See https://aka.ms/new-console-template for more information
using SICP;

class Reader : IReader
{
    public string Read()
    {
        while (true)
        {
            var value = Console.ReadLine()!;
            if (value == null)
                Thread.Sleep(100);
            else
                return value;
        }
    }
}