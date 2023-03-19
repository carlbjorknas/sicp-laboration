﻿namespace SICP;

public class Engine
{
    public string Eval(string expression)
    {
        if (IsSelfEvaluating(expression))
            return expression;

        if (IsApplication(expression))
        {
            var undressedEpxr = expression.Substring(1, expression.Length - 2);
            var op = GetOperator(undressedEpxr);
            var operands = GetOperands(undressedEpxr).ToList();
            operands = operands.Select(Eval).ToList();

            if (op == '+')
                return operands.Select(x => int.Parse(x)).Sum().ToString();
            else if (op == '-')
            {
                if (!operands.Any())
                    return 0.ToString();

                if (operands.Count == 1)
                    return (-int.Parse(operands[0])).ToString();

                var sum = operands.Skip(1).Select(int.Parse).Sum();
                return (int.Parse(operands[0]) - sum).ToString();
            }

        }
        
        throw new Exception($"Unknown expression type.'{expression}'");
    }

    private bool IsSelfEvaluating(string expression)
    {
        // TODO: Handle strings and decimals.
        return int.TryParse(expression, out var result);
    }

    private bool IsApplication(string expression)
    {
        return expression.StartsWith('(') && expression.EndsWith(')');
    }

    private char GetOperator(string code)
    {
        if (code.Length >= 1)
            return code[0];

        throw new Exception($"Could not find an operator in '{code}'.");
    }

    /// <summary>
    /// Outermost parentheses has been removed.
    /// Examples:
    /// + 1 2
    /// + (+ 1 2) 3
    /// </summary>
    /// <param name="undressedCode"></param>
    /// <returns></returns>
    private IEnumerable<string> GetOperands(string undressedCode)
    {
        if (undressedCode.Length == 1)
            yield break;

        var rest = undressedCode.Substring(2);
        while (rest.Any())
        {
            if (rest[0] == '(')
            {
                var index = FindIndexOfMatchingClosingParen(rest);
                yield return rest[..(index + 1)];
                rest = rest[(index + 1)..];
            } 
            else if (rest[0] == ' ')
            {
                rest = rest[1..];
            }
            else
            {
                var index = rest.IndexOf(' ');
                if (index == -1)
                {
                    yield return rest;
                    yield break;
                }

                var operand = rest[..index];
                rest = rest[(index + 1)..];
                yield return operand;
            }
        }
    }

    private int FindIndexOfMatchingClosingParen(string operandsString)
    {
        var numberUnclosed = 1;
        for (int i = 1; i < operandsString.Length; i++)
        {
            if (operandsString[i] == ')')
                if (numberUnclosed == 1)
                    return i;
                else
                {
                    numberUnclosed--;
                    if (numberUnclosed == 0)
                        throw new Exception($"Too many closing parens in '{operandsString}'");
                }
            else if (operandsString[i] == '(')
                numberUnclosed++;
        }

        throw new Exception($"Found no matching closing parenthesis in '{operandsString}'");
    }
}
