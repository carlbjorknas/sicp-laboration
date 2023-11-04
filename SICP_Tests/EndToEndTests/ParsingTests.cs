using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class ParsingTests : EndToEndTestBase
{
    [TestMethod]
    public void Evaluation_is_not_made_until_the_expression_is_complete()
    {
        SetupInputSequence(
            "(define (factorial n)           ",
            "   (if (= n 1)                  ",
            "       1                        ",
            "       (* n (factorial (- n 1)))",
            "   )                            ",
            ")                               ",
            "(factorial 5)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("120"), Times.Once);
    }

    [TestMethod]
    public void Can_handle_input_rows_with_whitespace_only()
    {
        SetupInputSequence(
            "(define",
            "",
            " ",
            "\t",
            "x",
            "10",
            ")");

        _sut!.Run();
        _printerMock!.Verify(x => x.Print("ok"), Times.Once);
    }
}
