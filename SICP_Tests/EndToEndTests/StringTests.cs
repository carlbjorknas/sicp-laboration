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
}
