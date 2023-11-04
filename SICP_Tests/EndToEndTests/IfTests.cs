using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class IfTests : EndToEndTestBase
{
    [TestMethod]
    public void An_if_expression_with_a_true_predicate_returns_its_consequent()
    {
        SetupInputSequence("(if true 1 2)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("1"), Times.Once);
    }

    [TestMethod]
    public void An_if_expression_with_a_false_predicate_returns_its_alternative()
    {
        SetupInputSequence("(if false 1 2)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("2"), Times.Once);
    }

    [TestMethod]
    public void When_an_if_expressions_predicate_is_false_and_it_has_no_alternative_then_false_is_returned()
    {
        SetupInputSequence("(if false 1)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("false"), Times.Once);
    }

    [TestMethod]
    public void Can_evaluate_an_if_epxression_having_complex_expression_predicate_and_consequent()
    {
        SetupInputSequence("(if (and true true) (+ 1 1))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("2"), Times.Once);
    }

    [TestMethod]
    public void When_the_predicate_is_a_non_boolean_it_is_evaluated_as_true()
    {
        SetupInputSequence("(if 1 2 3)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("2"), Times.Once);
    }
}
