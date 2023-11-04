using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SICP.Expressions;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class DefineTests : EndToEndTestBase
{
    [TestMethod]
    public void Can_define_a_variable_and_bind_it_to_a_number()
    {
        SetupInputSequence("(define x 10)");

        var env = _sut!.Run();

        _printerMock!.Verify(x => x.Print("ok"), Times.Once);
        var value = env.GetValue("x");
        CompareExpressions(value, new NumberExpression(10));
    }

    [TestMethod]
    public void A_variable_can_contain_letters_digits_hyphens_and_exclamation_mark()
    {
        SetupInputSequence("(define xyz123-! 10)");

        var env = _sut!.Run();

        _printerMock!.Verify(x => x.Print("ok"), Times.Once);
        var value = env.GetValue("xyz123-!");
        CompareExpressions(value, new NumberExpression(10));
    }

    [TestMethod]
    public void Can_define_a_variable_and_bind_it_to_the_result_of_an_addition_expression()
    {
        SetupInputSequence("(define x (+ 2 3))");

        var env = _sut!.Run();

        _printerMock!.Verify(x => x.Print("ok"), Times.Once);
        var value = env.GetValue("x");
        CompareExpressions(value, new NumberExpression(5));
    }

    [TestMethod]
    public void A_set_variable_returns_its_value()
    {
        SetupInputSequence(
            "(define x (+ 2 3))",
            "x");

        var env = _sut!.Run();

        _printerMock!.Verify(x => x.Print("ok"), Times.Once);
        _printerMock!.Verify(x => x.Print("5"), Times.Once);
    }

    [TestMethod]
    public void Can_use_a_variable_in_an_add_expression()
    {
        SetupInputSequence(
            "(define x (+ 2 3))",
            "(+ 1 x)");

        var env = _sut!.Run();

        _printerMock!.Verify(x => x.Print("ok"), Times.Once);
        _printerMock!.Verify(x => x.Print("6"), Times.Once);
    }

    [TestMethod]
    public void When_using_an_unbound_variable_an_exception_is_thrown()
    {
        SetupInputSequence("x");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("Variable 'x' is unbound."), Times.Once);
    }

    [TestMethod]
    public void Can_define_a_procedure_using_standard_procedure_definition()
    {
        SetupInputSequence(
            "(define (square x) (* x x))",
            "square");

        var env = _sut!.Run();

        _printerMock!.Verify(x => x.Print("ok"), Times.Once);
        _printerMock!.Verify(x => x.Print("(lambda (x) (* x x))"), Times.Once);
    }

    [TestMethod]
    public void Can_calculate_factorial_using_a_linear_recursive_process()
    {
        // Use recursion to calculate 5! = 1 * 2 * 3 * 4 * 5
        SetupInputSequence(
            "(define (factorial n) (if (= n 1) 1 (* n (factorial (- n 1)))))",
            "(factorial 5)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("120"), Times.Once);
    }
}
