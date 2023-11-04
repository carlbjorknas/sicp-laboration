using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class QuoteTests : EndToEndTestBase
{
    [TestMethod]
    public void Can_quote_a_symbol()
    {
        SetupInputSequence($"(quote abc)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("abc"), Times.Once);
    }

    [TestMethod]
    public void Can_quote_a_list()
    {
        SetupInputSequence($"(quote (a b c))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(a b c)"), Times.Once);
    }

    [TestMethod]
    public void Can_shorthandquote_a_symbol()
    {
        SetupInputSequence($"'abc");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("abc"), Times.Once);
    }

    [TestMethod]
    public void Can_shorthandquote_a_list()
    {
        SetupInputSequence($"'(a b c)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(a b c)"), Times.Once);
    }
}
