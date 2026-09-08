using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class EvalTests : EndToEndTestBase
{
    [TestMethod]
    public void Can_call_eval()
    {
        SetupInputSequence($"(eval (quote (+ 1 1)))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("2"), Times.Once);
    }

    [TestMethod]
    public void Eval_uses_the_current_environment()
    {
        SetupInputSequence("(define x 5)", "(eval (quote x))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("5"), Times.Once);
    }

    [TestMethod]
    public void Eval_can_use_a_procedure_defined_by_the_user()
    {
        SetupInputSequence("(define (square n) (* n n))", "(eval (quote (square 4)))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("16"), Times.Once);
    }

    [TestMethod]
    public void Eval_of_an_unbound_symbol_reports_it_as_unbound()
    {
        SetupInputSequence("(eval (quote nope))");
        _sut!.Run();
        _printerMock!.Verify(
            x => x.Print(It.Is<string>(s => s.Contains("nope") && s.Contains("unbound"))),
            Times.Once);
    }
}
