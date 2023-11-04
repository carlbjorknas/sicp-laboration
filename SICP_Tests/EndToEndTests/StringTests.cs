using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class StringTests : EndToEndTestBase
{
    [TestMethod]
    public void A_string_is_self_evaluated()
    {
        SetupInputSequence("\"a_string\"");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("\"a_string\""), Times.Once);
    }

    [TestMethod]
    public void A_string_can_contain_spaces_and_tabs()
    {
        SetupInputSequence("\" \t\"");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("\" \t\""), Times.Once);
    }

    [TestMethod]
    public void A_string_can_contain_escaped_double_quotes()
    {
        SetupInputSequence("\"The \\\"fresh\\\" bread was all dried up.\"");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("\"The \\\"fresh\\\" bread was all dried up.\""), Times.Once);
    }
}
