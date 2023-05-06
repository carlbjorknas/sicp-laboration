using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SICP;

namespace SICP_Tests;

[TestClass]
public class LexerTests
{
    [TestMethod]
    public void When_lexing_the_string_true_a_bool_token_having_the_value_true_is_returned()
    {
        var sut = new Lexer();
        var result = sut.Tokenize("true");
        result.Should().HaveCount(1);
        result[0].Should().BeOfType<BoolToken>();
        ((BoolToken)result[0]).Value.Should().BeTrue();
    }

    [TestMethod]
    public void When_lexing_the_string_123_a_number_token_having_the_value_123_is_returned()
    {
        var sut = new Lexer();
        var result = sut.Tokenize("123");
        result.Should().HaveCount(1);
        result[0].Should().BeOfType<NumberToken>();
        ((NumberToken)result[0]).Value.Should().Be(123);
    }

    [TestMethod]
    public void When_lexing_a_plus_sign_an_identifier_token_having_the_value_plus_sign_is_returned()
    {
        var sut = new Lexer();
        var result = sut.Tokenize("+");
        result.Should().HaveCount(1);
        result[0].Should().BeOfType<IdentifierToken>();
        ((IdentifierToken)result[0]).Value.Should().Be("+");
    }

    [TestMethod]
    public void When_lexing_a_call_to_plus_without_operands_then_three_tokens_are_returned()
    {
        var sut = new Lexer();
        var result = sut.Tokenize("(+)");
        result.Should().HaveCount(3);
        result[0].Should().BeOfType<PunctuatorToken>();
        result[1].Should().BeOfType<IdentifierToken>();
        result[2].Should().BeOfType<PunctuatorToken>();
        ((PunctuatorToken)result[0]).Value.Should().Be("(");
        ((IdentifierToken)result[1]).Value.Should().Be("+");
        ((PunctuatorToken)result[2]).Value.Should().Be(")");
    }

    [TestMethod]
    public void Lexing_an_addition_of_two_numbers()
    {
        var sut = new Lexer();
        var result = sut.Tokenize("(+ 2 3)");
        result.Should().HaveCount(5);
        result[0].Should().BeOfType<PunctuatorToken>();
        result[1].Should().BeOfType<IdentifierToken>();
        result[2].Should().BeOfType<NumberToken>();
        result[3].Should().BeOfType<NumberToken>();
        result[4].Should().BeOfType<PunctuatorToken>();
        ((PunctuatorToken)result[0]).Value.Should().Be("(");
        ((IdentifierToken)result[1]).Value.Should().Be("+");
        ((NumberToken)result[2]).Value.Should().Be(2);
        ((NumberToken)result[3]).Value.Should().Be(3);
        ((PunctuatorToken)result[4]).Value.Should().Be(")");
    }

    [TestMethod]
    public void Lexing_an_unary_subtraction()
    {
        var sut = new Lexer();
        var result = sut.Tokenize("(- 2)");
        result.Should().HaveCount(4);
        result[0].Should().BeOfType<PunctuatorToken>();
        result[1].Should().BeOfType<IdentifierToken>();
        result[2].Should().BeOfType<NumberToken>();
        result[3].Should().BeOfType<PunctuatorToken>();
        ((PunctuatorToken)result[0]).Value.Should().Be("(");
        ((IdentifierToken)result[1]).Value.Should().Be("-");
        ((NumberToken)result[2]).Value.Should().Be(2);
        ((PunctuatorToken)result[3]).Value.Should().Be(")");
    }
}
