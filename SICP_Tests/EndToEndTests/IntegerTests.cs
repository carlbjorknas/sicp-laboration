using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class IntegerTests : EndToEndTestBase
{
    [TestMethod]
    public void Integers_are_self_evaluated()
    {
        SetupInputSequence("123");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("123"), Times.Once);
    }

    [TestMethod]
    [DataRow("<", 1, 1, false)]
    [DataRow("<", 2, 1, false)]
    [DataRow("<", 1, 2, true)]
    [DataRow("<=", 1, 1, true)]
    [DataRow("<=", 2, 1, false)]
    [DataRow("<=", 1, 2, true)]
    [DataRow("=", 1, 1, true)]
    [DataRow("=", 1, 2, false)]
    [DataRow("=", 2, 1, false)]
    [DataRow(">=", 1, 1, true)]
    [DataRow(">=", 1, 2, false)]
    [DataRow(">=", 2, 1, true)]
    [DataRow(">", 1, 1, false)]
    [DataRow(">", 1, 2, false)]
    [DataRow(">", 2, 1, true)]
    public void Can_do_arithmetic_comparisons(string op, int arg1, int arg2, bool expectedResult)
    {
        SetupInputSequence($"({op} {arg1} {arg2})");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print(expectedResult.ToString().ToLower()), Times.Once);
    }
}
