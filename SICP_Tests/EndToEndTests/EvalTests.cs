using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class EvalTests : EndToEndTestBase
{
    // I´m not sure how eval is supposed to work really, but wanted a fast hack
    // to evaluate the result from a "quote".
    // The user should be able to pass an environment also.
    [TestMethod]
    public void Can_call_eval()
    {
        SetupInputSequence($"(eval (quote (+ 1 1)))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("2"), Times.Once);
    }
}
