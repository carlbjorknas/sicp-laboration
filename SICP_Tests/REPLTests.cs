using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SICP;
using SICP.Exceptions;
using System.Linq;

namespace SICP_Tests;

[TestClass]
public class REPLTests : TestBase
{
    private Mock<IReader>? _readerMock;
    private Mock<IPrinter>? _printerMock;
    private REPL? _sut;

    [TestInitialize]
    public void Init()
    {
        _readerMock = new Mock<IReader>();
        _printerMock = new Mock<IPrinter>();
        var lexer = new Lexer();
        var parser = new Parser();
        var evaluator = new Evaluator();
        _sut = new REPL(_readerMock.Object, _printerMock.Object, lexer, parser, evaluator);
    }

    private void SetupInputSequence(params string[] inputs)
    {
        var sequence = _readerMock!.SetupSequence(x => x.Read());
        inputs.Append("")
            .ToList()            
            .ForEach(x => sequence = sequence.Returns(x));
    }

    [TestMethod]
    public void When_empty_string_is_entered_the_repl_ends()
    {
        _readerMock!.Setup(x => x.Read()).Returns("");
        _sut!.Run();
        _readerMock.Verify(x => x.Read(), Times.Once);
    }

    [TestMethod]
    public void When_true_is_entered_true_is_printed()
    {
        SetupInputSequence("true");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("true"), Times.Once);
    }

    [TestMethod]
    public void When_false_is_entered_false_is_printed()
    {
        SetupInputSequence("false");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("false"), Times.Once);
    }

    [TestMethod]
    public void When_a_integer_is_entered_it_is_printed_back()
    {
        SetupInputSequence("123");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("123"), Times.Once);
    }

    [TestMethod]
    public void When_entering_a_plus_then_the_string_PrimitiveProcedure_is_printed()
    {
        SetupInputSequence("+");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("PrimitiveProcedure"), Times.Once);
    }

    [TestMethod]
    public void When_calling_plus_without_operators_then_zero_is_printed()
    {
        SetupInputSequence("(+)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("0"), Times.Once);
    }

    [TestMethod]
    public void Can_add_two_numbers()
    {
        SetupInputSequence("(+ 2 3)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("5"), Times.Once);
    }

    [TestMethod]
    public void Can_add_two_addition_expressions()
    {
        SetupInputSequence("(+ (+ 1 2) (+ 3 4))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("10"), Times.Once);
    }

    [TestMethod]
    public void Can_add_when_operand_is_nested_four_levels()
    {
        SetupInputSequence("(+ (+ (+ (+ (+ 1 2)))))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("3"), Times.Once);
    }

    [TestMethod]
    public void Subtraction_without_arguments_returns_0()
    {
        SetupInputSequence("(-)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("0"), Times.Once);
    }

    [TestMethod]
    public void When_given_a_unary_subtraction_of_a_positive_number_it_is_returned_with_a_minus()
    {
        SetupInputSequence("(- 2)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("-2"), Times.Once);
    }

    [TestMethod]
    public void When_given_a_subtraction_with_two_numbers_their_diff_is_returned()
    {
        SetupInputSequence("(- 2 1)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("1"), Times.Once);
    }

    [TestMethod]
    public void When_given_a_subtraction_with_three_numbers_the_first_is_reduced_by_the_two_others()
    {
        SetupInputSequence("(- 10 3 4)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("3"), Times.Once);
    }

    [TestMethod]
    public void Math_operation_with_operands_where_some_are_numbers_and_some_are_expressions()
    {
        SetupInputSequence("(+ 1 (+ 2 3) (- 4 1) 2)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print((1 + 2 + 3 + (4 - 1) + 2).ToString()), Times.Once);
    }

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
    public void When_using_an_unbound_variable_an_exception_is_thrown()
    {
        SetupInputSequence("x");

        // TODO The exception should be catched and written to output instead.
        _sut!.Invoking(x => x.Run())
            .Should().Throw<UnboundVariableException>()
            .WithMessage("Variable 'x' is unbound.");
    }
}
