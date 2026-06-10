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
}
