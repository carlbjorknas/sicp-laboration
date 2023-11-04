using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class OrTests : EndToEndTestBase
{
    [TestMethod]
    public void Or_without_arguments_returns_false()
    {
        SetupInputSequence("(or)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("false"), Times.Once);
    }

    [TestMethod]
    [DataRow("false", "false", "true", "true")]
    [DataRow("false", "1", "true", "1")]
    [DataRow("false", "false", "false", "false")]
    public void Or_returns_first_non_false_argument_or_else_false(string arg1, string arg2, string arg3, string expected)
    {
        SetupInputSequence($"(or {arg1} {arg2} {arg3})");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print(expected), Times.Once);
    }

    [TestMethod]
    public void Or_does_not_evaluate_arguments_coming_after_a_non_false()
    {
        // if "a" was evaluated it would result in a UnboundVariableException.
        SetupInputSequence($"(or 1 a)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("1"), Times.Once);
    }
}
