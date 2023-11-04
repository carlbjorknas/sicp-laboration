using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class BoolTests : EndToEndTestBase
{
    [TestMethod]
    public void True_is_self_evaluated()
    {
        SetupInputSequence("true");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("true"), Times.Once);
    }

    [TestMethod]
    public void False_is_self_evaluated()
    {
        SetupInputSequence("false");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("false"), Times.Once);
    }
}
