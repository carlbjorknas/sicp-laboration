using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SICP;

namespace SICP_Tests;

[TestClass]
public class ParserTests
{
    [TestMethod]
    public void When_parsing_a_true_bool_token_a_bool_expression_having_the_value_true_is_returned()
    {
        var sut = new Parser();
        var result = sut.Parse(new[] { new BoolToken(true) });

        result.Should().NotBeNull()
            .And.BeOfType<BooleanExpression>();
        ((BooleanExpression)result).Value.Should().BeTrue();        
    }
}
