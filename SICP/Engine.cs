﻿using SICP.EvalResults;
using System.Text.RegularExpressions;

namespace SICP;

public class Engine
{
    public EvalResult Eval(string expression, Environment env)
    {
        if (IsSelfEvaluating(expression, out var evalResult))
            return evalResult!;

        if (IsVariable(expression))
            return env.GetValue(expression);

        if (IsDefinition(expression))
            return HandleDefinition(expression, env);

        if (IsApplication(expression))
        {            
            var op = GetOperator(expression);
            var operands = GetOperands(expression).ToList();
            return Apply(op, operands, env);
        }
        
        throw new Exception($"Unknown expression type.'{expression}'");
    }

    private EvalResult Apply(string op, List<string> operands, Environment env)
    {
        var evaluatedOperands = operands.Select(operand => Eval(operand, env)).ToList();

        if (op == "+")
            return new IntEvalResult(evaluatedOperands.Cast<IntEvalResult>().Sum(x => x.Value));
        else if (op == "-")
        {
            if (!operands.Any())
                return new IntEvalResult(0);

            if (operands.Count == 1)
                return new IntEvalResult(-((IntEvalResult)evaluatedOperands[0]).Value);

            var sum = evaluatedOperands.Skip(1).Cast<IntEvalResult>().Sum(x => x.Value);
            return new IntEvalResult(((IntEvalResult)evaluatedOperands[0]).Value - sum);
        }
        throw new Exception($"Uknown operator '{op}'");
    }

    private bool IsSelfEvaluating(string expression, out EvalResult? evalResult)
    {
        evalResult = null;

        // TODO: Handle strings and decimals.
        if (int.TryParse(expression, out var result))
        {
            evalResult = new IntEvalResult(result);
            return true;
        }

        return false;
    }

    private bool IsVariable(string expression)
    {
        return Regex.IsMatch(expression, "^[a-z0-9-!]+$", RegexOptions.IgnoreCase);
    }

    private bool IsDefinition(string expression)
    {
        return GetOperator(expression) == "define";
    }

    private bool IsApplication(string expression)
    {
        return expression.StartsWith('(') && expression.EndsWith(')');
    }

    private static string GetOperator(string expression)
    {
        var op = expression
            .Skip(1) // the "("
            .TakeWhile(x => x is not (' ' or ')'))
            .ToArray();
        return new string(op);
    }

    private IEnumerable<string> GetOperands(string expression)
    {
        var operandsCharArr = expression
            .Skip(1) // the "("
            .Skip(GetOperator(expression).Length)
            .SkipLast(1) // the ")"
            .ToArray();

        var operandsStr = new string(operandsCharArr);
        while (operandsStr.Any())
        {
            if (operandsStr[0] == '(')
            {
                var index = FindIndexOfMatchingClosingParen(operandsStr);
                yield return operandsStr[..(index + 1)];
                operandsStr = operandsStr[(index + 1)..];
            } 
            else if (operandsStr[0] == ' ')
            {
                operandsStr = operandsStr[1..];
            }
            else
            {
                var index = operandsStr.IndexOf(' ');
                if (index == -1)
                {
                    yield return operandsStr;
                    yield break;
                }

                var operand = operandsStr[..index];
                operandsStr = operandsStr[(index + 1)..];
                yield return operand;
            }
        }

        int FindIndexOfMatchingClosingParen(string operandsString)
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

    private EvalResult HandleDefinition(string expression, Environment env)
    {
        var operands = GetOperands(expression).ToList();

        if (operands.Count != 2)
            throw new Exception($"Invalid number of operands for 'define' in '{expression}'");

        var variableName = operands.First();
        // TODO Fix temporary hack. Call Eval instead of this.
        var variableValue = new IntEvalResult(int.Parse(operands.Last()));
        env.AddVariable(variableName, variableValue);

        return new SymbolEvalResult("ok");
    }
}
