using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SICP;

namespace SICP_Tests;

[TestClass]
public class REPLTests
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
        _readerMock!.SetupSequence(x => x.Read())
            .Returns("true")
            .Returns("");

        _sut!.Run();

        _printerMock!.Verify(x => x.Print("true"), Times.Once);
    }

    [TestMethod]
    public void When_false_is_entered_false_is_printed()
    {
        _readerMock!.SetupSequence(x => x.Read())
            .Returns("false")
            .Returns("");

        _sut!.Run();

        _printerMock!.Verify(x => x.Print("false"), Times.Once);
    }

    [TestMethod]
    public void When_a_integer_is_entered_it_is_printed_back()
    {
        _readerMock!.SetupSequence(x => x.Read())
            .Returns("123")
            .Returns("");

        _sut!.Run();

        _printerMock!.Verify(x => x.Print("123"), Times.Once);
    }

    [TestMethod]
    public void When_entering_a_plus_then_the_string_PrimitiveProcedure_is_printed()
    {
        _readerMock!.SetupSequence(x => x.Read())
            .Returns("+")
            .Returns("");

        _sut!.Run();

        _printerMock!.Verify(x => x.Print("PrimitiveProcedure"), Times.Once);
    }

    [TestMethod]
    public void When_calling_plus_without_operators_then_zero_is_printed()
    {
        _readerMock!.SetupSequence(x => x.Read())
            .Returns("(+)")
            .Returns("");

        _sut!.Run();

        _printerMock!.Verify(x => x.Print("0"), Times.Once);
    }

    [TestMethod]
    public void Can_add_two_numbers()
    {
        _readerMock!.SetupSequence(x => x.Read())
            .Returns("(+ 2 3)")
            .Returns("");

        _sut!.Run();

        _printerMock!.Verify(x => x.Print("5"), Times.Once);
    }

    [TestMethod]
    public void Can_add_two_addition_expressions()
    {
        _readerMock!.SetupSequence(x => x.Read())
            .Returns("(+ (+ 1 2) (+ 3 4))")
            .Returns("");

        _sut!.Run();

        _printerMock!.Verify(x => x.Print("10"), Times.Once);
    }

    [TestMethod]
    public void Can_add_when_operand_is_nested_four_levels()
    {
        _readerMock!.SetupSequence(x => x.Read())
            .Returns("(+ (+ (+ (+ (+ 1 2)))))")
            .Returns("");

        _sut!.Run();

        _printerMock!.Verify(x => x.Print("3"), Times.Once);
    }

    [TestMethod]
    public void Subtraction_without_arguments_returns_0()
    {
        _readerMock!.SetupSequence(x => x.Read())
            .Returns("(-)")
            .Returns("");

        _sut!.Run();

        _printerMock!.Verify(x => x.Print("0"), Times.Once);
    }

    [TestMethod]
    public void When_given_a_unary_subtraction_of_a_positive_number_it_is_returned_with_a_minus()
    {
        _readerMock!.SetupSequence(x => x.Read())
            .Returns("(- 2)")
            .Returns("");

        _sut!.Run();

        _printerMock!.Verify(x => x.Print("-2"), Times.Once);
    }

    [TestMethod]
    public void When_given_a_subtraction_with_two_numbers_their_diff_is_returned()
    {
        _readerMock!.SetupSequence(x => x.Read())
            .Returns("(- 2 1)")
            .Returns("");

        _sut!.Run();

        _printerMock!.Verify(x => x.Print("1"), Times.Once);
    }

    [TestMethod]
    public void When_given_a_subtraction_with_three_numbers_the_first_is_reduced_by_the_two_others()
    {
        _readerMock!.SetupSequence(x => x.Read())
            .Returns("(- 10 3 4)")
            .Returns("");

        _sut!.Run();

        _printerMock!.Verify(x => x.Print("3"), Times.Once);
    }

    [TestMethod]
    public void Math_operation_with_operands_where_some_are_numbers_and_some_are_expressions()
    {
        _readerMock!.SetupSequence(x => x.Read())
            .Returns("(+ 1 (+ 2 3) (- 4 1) 2)")
            .Returns("");

        _sut!.Run();

        _printerMock!.Verify(x => x.Print((1 + 2 + 3 + (4 - 1) + 2).ToString()), Times.Once);
    }

    [TestMethod]
    public void Can_define_a_variable_and_bind_it_to_a_number()
    {
        _readerMock!.SetupSequence(x => x.Read())
            .Returns("(define x 10)")
            .Returns("");

        var env = _sut!.Run();

        _printerMock!.Verify(x => x.Print("ok"), Times.Once);
        var value = env.GetValue("x");
        value.Should().BeOfType<NumberExpression>()
            .Which.Value.Should().Be(10);
    }
}
