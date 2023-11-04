using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class LambdaTests : EndToEndTestBase
{
    [TestMethod]
    public void Can_make_a_lambda()
    {
        SetupInputSequence("(lambda (x) (* x x))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(lambda (x) (* x x))"), Times.Once);
    }

    [TestMethod]
    public void Can_apply_a_lambda()
    {
        SetupInputSequence("((lambda (x) (* x x)) 2)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("4"), Times.Once);
    }
}
