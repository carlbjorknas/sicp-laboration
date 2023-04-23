using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SICP;

namespace SICP_Tests;

[TestClass]
public class REPLTests
{
    private Mock<IReader> _readerMock;
    private Mock<IPrinter> _printerMock;
    private REPL _sut;

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
        _readerMock.Setup(x => x.Read()).Returns("");

        _sut.Run();

        _readerMock.Verify(x => x.Read(), Times.Once);
    }

    [TestMethod]
    public void When_true_is_entered_true_is_printed()
    {        
        _readerMock.SetupSequence(x => x.Read())
            .Returns("true")
            .Returns("");

        _sut.Run();

        _printerMock.Verify(x => x.Print("true"), Times.Once);
    }
}
