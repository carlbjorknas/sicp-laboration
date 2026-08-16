using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class DivisionTests : EndToEndTestBase
{
    [TestMethod]
    public void Division_of_two_numbers_returns_quotient()
    {
        SetupInputSequence("(/ 10 2)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("5"), Times.Once);
    }

    [TestMethod]
    public void Division_with_negative_dividend_returns_negative_quotient()
    {
        SetupInputSequence("(/ -6 2)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("-3"), Times.Once);
    }

    [TestMethod]
    public void Division_that_does_not_divide_evenly_truncates_towards_zero()
    {
        // Documents current behaviour: NumberExpression.Value is an int, so the
        // result is truncated rather than rounded or kept as a fraction.
        SetupInputSequence("(/ 1 2)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("0"), Times.Once);
    }

    [TestMethod]
    public void Division_without_operands_throws()
    {
        SetupInputSequence("(/)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("'/' expects at least 1 operand(s), got 0"), Times.Once);
    }

    [TestMethod]
    public void Division_with_a_single_operand_returns_its_reciprocal()
    {
        SetupInputSequence("(/ 1)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("1"), Times.Once);
    }

    [TestMethod]
    public void Division_with_a_single_operand_greater_than_1_truncates_its_reciprocal_to_0()
    {
        // Documents current behaviour: reciprocal of an int > 1 truncates to 0,
        // same int-truncation as Division_that_does_not_divide_evenly_truncates_towards_zero.
        SetupInputSequence("(/ 5)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("0"), Times.Once);
    }
}
