using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class NotTests : EndToEndTestBase
{
    [TestMethod]
    [DataRow("true")]
    [DataRow("0")]
    [DataRow("1")]
    public void Not_inverts_truthy_to_false(string value)
    {
        SetupInputSequence($"(not {value})");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("false"), Times.Once);
    }

    [TestMethod]
    public void Not_inverts_false_to_true()
    {
        SetupInputSequence("(not false)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("true"), Times.Once);
    }
}
