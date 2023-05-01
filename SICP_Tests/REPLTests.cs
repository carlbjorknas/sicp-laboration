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
}
