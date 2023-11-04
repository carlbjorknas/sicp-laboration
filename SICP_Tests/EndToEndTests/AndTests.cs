using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class AndTests : EndToEndTestBase
{
    [TestMethod]
    public void And_without_arguments_returns_true()
    {
        SetupInputSequence("(and)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("true"), Times.Once);
    }

    [TestMethod]
    [DataRow("false", "true", "true")]
    [DataRow("true", "false", "true")]
    [DataRow("true", "true", "false")]
    public void And_with_a_false_argument_returns_false(string arg1, string arg2, string arg3)
    {
        SetupInputSequence($"(and {arg1} {arg2} {arg3})");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("false"), Times.Once);
    }

    [TestMethod]
    [DataRow("true", "true", "true")]
    [DataRow("1", "2", "3")]
    public void And_without_a_false_argument_returns_its_last_argument(string arg1, string arg2, string arg3)
    {
        SetupInputSequence($"(and {arg1} {arg2} {arg3})");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print(arg3), Times.Once);
    }

    [TestMethod]
    public void And_does_not_evaluate_arguments_coming_after_a_false()
    {
        // if "a" was evaluated it would result in a UnboundVariableException.
        SetupInputSequence($"(and false a)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("false"), Times.Once);
    }
}
