using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class HelpTests : EndToEndTestBase
{
    [TestMethod]
    public void Help_returns_a_list_of_the_available_primitives_and_special_forms()
    {
        SetupInputSequence("(help)");
        _sut!.Run();
        _printerMock!.Verify(
            x => x.Print(It.Is<string>(s =>
                s.StartsWith("(") && s.EndsWith(")")
                && s.Contains("car") && s.Contains("quit")   // primitiver
                && s.Contains("if") && s.Contains("lambda"))), // specialformer
            Times.Once);
    }

    [TestMethod]
    public void Help_includes_names_the_user_has_defined()
    {
        SetupInputSequence("(define my-var 10)", "(help)");
        _sut!.Run();
        _printerMock!.Verify(
            x => x.Print(It.Is<string>(s => s.StartsWith("(") && s.Contains("my-var"))),
            Times.Once);
    }

    [TestMethod]
    public void Help_with_an_argument_throws()
    {
        SetupInputSequence("(help 1)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("'help' expects 0 operand(s), got 1"), Times.Once);
    }
}
