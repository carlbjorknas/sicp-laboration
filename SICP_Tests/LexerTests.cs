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
}
