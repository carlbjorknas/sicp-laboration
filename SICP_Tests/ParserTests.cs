﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SICP;

namespace SICP_Tests;

[TestClass]
public class ParserTests
{
    [TestMethod]
    public void When_parsing_a_true_bool_token_then_a_bool_expression_having_the_value_true_is_returned()
    {
        var sut = new Parser();
        var result = sut.Parse(new[] { new BoolToken(true) });

        result.Should().NotBeNull()
            .And.BeOfType<BooleanExpression>();
        ((BooleanExpression)result).Value.Should().BeTrue();        
    }

    [TestMethod]
    public void When_parsing_a_number_token_then_a_number_expression_having_the_correct_value_is_returned()
    {
        var sut = new Parser();
        var result = sut.Parse(new[] { new NumberToken(123) });

        result.Should().NotBeNull()
            .And.BeOfType<NumberExpression>();
        ((NumberExpression)result).Value.Should().Be(123);
    }

    [TestMethod]
    public void When_parsing_the_identifier_token_plus_then_a_variable_expression_having_a_plus_as_value_is_returned()
    {
        var sut = new Parser();
        var result = sut.Parse(new[] { new IdentifierToken("+") });

        result.Should().NotBeNull()
            .And.BeOfType<VariableExpression>();
        ((VariableExpression)result).Value.Should().Be("+");
    }

    [TestMethod]
    public void When_parsing_an_addition_without_operands_then_a_procedure_call_expression_having_plus_as_operator_and_no_operands_is_returned()
    {
        var sut = new Parser();
        var tokens = new Token[]
        {
            new PunctuatorToken("("),
            new IdentifierToken("+"),
            new PunctuatorToken(")")
        };
        var result = sut.Parse(tokens);

        result.Should().NotBeNull()
            .And.BeOfType<ProcedureCallExpression>();

        var call = (ProcedureCallExpression)result;
        call.Operator.Should().BeOfType<VariableExpression>();
        ((VariableExpression)call.Operator).Value.Should().Be("+");

        call.Operands.Should().BeEmpty();
    }
}
