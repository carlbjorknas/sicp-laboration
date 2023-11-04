using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SICP;
using System.Linq;

namespace SICP_Tests.EndToEndTests;

public class EndToEndTestBase : TestBase
{
    protected Mock<IReader>? _readerMock;
    protected Mock<IPrinter>? _printerMock;
    protected REPL? _sut;

    [TestInitialize]
    public void Init()
    {
        _readerMock = new Mock<IReader>();
        _printerMock = new Mock<IPrinter>();
        var lexer = new Lexer(_readerMock.Object);
        var parser = new Parser(lexer);
        var evaluator = new Evaluator();
        _sut = new REPL(_printerMock.Object, parser, evaluator);
    }

    protected void SetupInputSequence(params string[] inputs)
    {
        var sequence = _readerMock!.SetupSequence(x => x.Read());
        inputs.Append("(quit)")
            .ToList()
            .ForEach(x => sequence = sequence.Returns(x));
    }
}
