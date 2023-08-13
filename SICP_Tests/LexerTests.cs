using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SICP;

namespace SICP_Tests;

[TestClass]
public class LexerTests
{
    private Lexer CreateSut(string command)
    {
        var readerMock = new Mock<IReader>();
        var sequence = readerMock!.SetupSequence(x => x.Read());
        sequence.Returns(command);

        return new Lexer(readerMock.Object);
    }

    [TestMethod]
    public void When_lexing_the_string_true_a_bool_token_having_the_value_true_is_returned()
    {
        CreateSut("true").GetNextToken()
            .Should().BeOfType<BoolToken>()
            .Which.Value.Should().BeTrue();
    }

    [TestMethod]
    public void When_lexing_the_string_123_a_number_token_having_the_value_123_is_returned()
    {
        CreateSut("123").GetNextToken()
            .Should().BeOfType<NumberToken>()
            .Which.Value.Should().Be(123);
    }

    [TestMethod]
    public void When_lexing_a_plus_sign_an_identifier_token_having_the_value_plus_sign_is_returned()
    {
        CreateSut("+").GetNextToken()
            .Should().BeOfType<IdentifierToken>()
            .Which.Value.Should().Be("+");
    }

    [TestMethod]
    public void When_lexing_a_call_to_plus_without_operands_then_three_tokens_are_returned()
    {
        var sut = CreateSut("(+)");

        sut.GetNextToken()
            .Should().BeOfType<PunctuatorToken>()
            .Which.Value.Should().Be("(");
        sut.GetNextToken()
            .Should().BeOfType<IdentifierToken>()
            .Which.Value.Should().Be("+");
        sut.GetNextToken()
            .Should().BeOfType<PunctuatorToken>()
            .Which.Value.Should().Be(")");
    }

    [TestMethod]
    public void Lexing_an_addition_of_two_numbers()
    {       
        var sut = CreateSut("(+ 2 3)");

        sut.GetNextToken()
            .Should().BeOfType<PunctuatorToken>()
            .Which.Value.Should().Be("(");
        sut.GetNextToken()
            .Should().BeOfType<IdentifierToken>()
            .Which.Value.Should().Be("+");
        sut.GetNextToken()
            .Should().BeOfType<NumberToken>()
            .Which.Value.Should().Be(2);
        sut.GetNextToken()
            .Should().BeOfType<NumberToken>()
            .Which.Value.Should().Be(3);
        sut.GetNextToken()
            .Should().BeOfType<PunctuatorToken>()
            .Which.Value.Should().Be(")");

    }

    [TestMethod]
    public void Lexing_an_unary_subtraction()
    {
        var sut = CreateSut("(- 2)");

        sut.GetNextToken()
            .Should().BeOfType<PunctuatorToken>()
            .Which.Value.Should().Be("(");
        sut.GetNextToken()
            .Should().BeOfType<IdentifierToken>()
            .Which.Value.Should().Be("-");
        sut.GetNextToken()
            .Should().BeOfType<NumberToken>()
            .Which.Value.Should().Be(2);
        sut.GetNextToken()
            .Should().BeOfType<PunctuatorToken>()
            .Which.Value.Should().Be(")");
    }

    [TestMethod]
    public void Lexing_a_definition()
    {
        var sut = CreateSut("(define x 10)");

        sut.GetNextToken()
            .Should().BeOfType<PunctuatorToken>()
            .Which.Value.Should().Be("(");
        sut.GetNextToken()
            .Should().BeOfType<IdentifierToken>()
            .Which.Value.Should().Be("define");
        sut.GetNextToken()
            .Should().BeOfType<IdentifierToken>()
            .Which.Value.Should().Be("x");
        sut.GetNextToken()
            .Should().BeOfType<NumberToken>()
            .Which.Value.Should().Be(10);
        sut.GetNextToken()
            .Should().BeOfType<PunctuatorToken>()
            .Which.Value.Should().Be(")");
    }
}
