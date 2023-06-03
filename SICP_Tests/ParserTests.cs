using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SICP;
using SICP.Expressions;

namespace SICP_Tests;

[TestClass]
public class ParserTests : TestBase
{ 
    [TestMethod]
    public void When_parsing_a_true_bool_token_then_a_bool_expression_having_the_value_true_is_returned()
    {
        var sut = new Parser();
        var result = sut.Parse(new[] { new BoolToken(true) });
        CompareExpressions(result, new BooleanExpression(true));
    }

    [TestMethod]
    public void When_parsing_a_number_token_then_a_number_expression_having_the_correct_value_is_returned()
    {
        var sut = new Parser();
        var result = sut.Parse(new[] { new NumberToken(123) });

        CompareExpressions(result, new NumberExpression(123));
    }

    [TestMethod]
    public void When_parsing_the_identifier_token_plus_then_a_variable_expression_having_a_plus_as_value_is_returned()
    {
        var sut = new Parser();
        var result = sut.Parse(new[] { new IdentifierToken("+") });
        CompareExpressions(result, new VariableExpression("+"));
    }

    [TestMethod]
    public void When_parsing_an_addition_without_operands_then_a_list_expression_is_returned()
    {
        var sut = new Parser();
        var tokens = new Token[]
        {
            new PunctuatorToken("("),
            new IdentifierToken("+"),
            new PunctuatorToken(")")
        };
        var result = sut.Parse(tokens); 
        
        CompareLists(result, CreateList(new VariableExpression("+")));
    }

    [TestMethod]
    public void Can_parse_an_addition_with_two_numbers()
    {
        var sut = new Parser();
        var tokens = new Token[]
        {
            new PunctuatorToken("("),
            new IdentifierToken("+"),
            new NumberToken(2),
            new NumberToken(3),
            new PunctuatorToken(")")
        };
        var result = sut.Parse(tokens);

        var expectedList = CreateList(
            new VariableExpression("+"),
            new NumberExpression(2),
            new NumberExpression(3)
         );

        CompareLists(result, expectedList);        
    }

    [TestMethod]
    public void Can_parse_an_unary_subtraction()
    {
        var sut = new Parser();
        var tokens = new Token[]
        {
            new PunctuatorToken("("),
            new IdentifierToken("-"),
            new NumberToken(2),
            new PunctuatorToken(")")
        };
        var result = sut.Parse(tokens);

        var expectedList = CreateList(new VariableExpression("-"), new NumberExpression(2));
        CompareLists(result, expectedList);
    }

    [TestMethod]
    public void Can_parse_a_definition_of_a_number()
    {
        var sut = new Parser();
        var tokens = new Token[]
        {
            new PunctuatorToken("("),
            new IdentifierToken("define"),
            new IdentifierToken("x"),
            new NumberToken(10),
            new PunctuatorToken(")")
        };
        var result = sut.Parse(tokens);
        var expectedList = CreateList(
            new VariableExpression("define"),
            new VariableExpression("x"),
            new NumberExpression(10)
        );
        CompareLists(result, expectedList);
    }
}
