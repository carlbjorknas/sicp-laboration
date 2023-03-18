using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SICP
{
    public class Engine
    {
        public string Eval(string code)
        {
            if (code.StartsWith("("))
            {
                if (!code.EndsWith(")"))
                    throw new Exception($"Expression did not end with a closing parenthesis. '{code}'");

                var undressedEpxr = code.Substring(1, code.Length - 2);
                var op = GetOperator(undressedEpxr);
                var operands = GetOperands(undressedEpxr);
                return operands.Select(x => int.Parse(x)).Sum().ToString();
            }

            return code;
        }

        private char GetOperator(string code)
        {
            if (code.Length >= 1)
                return code[0];

            throw new Exception($"Could not find an operator in '{code}'.");
        }

        private string[] GetOperands(string code)
        {
            return code.Split(" ").Skip(1).ToArray();
        }
    }
}
