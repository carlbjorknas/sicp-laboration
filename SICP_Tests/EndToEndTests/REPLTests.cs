using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class REPLTests : EndToEndTestBase
{
    [TestMethod]
    public void When_quit_is_called_the_repl_ends()
    {
        _readerMock!.Setup(x => x.Read()).Returns("(quit)");
        _sut!.Run();
        _readerMock.Verify(x => x.Read(), Times.Once);
    }
}
