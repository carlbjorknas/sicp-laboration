using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class MapTests : EndToEndTestBase
{
    [TestMethod]
    public void When_mapping_over_the_empty_list_the_empty_list_is_returned()
    {
        SetupInputSequence($"(map (lambda (x) (* x 2)) '())");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("()"), Times.Once);
    }

    [TestMethod]
    public void Can_map_over_a_single_list()
    {
        SetupInputSequence($"(map (lambda (x) (* x 2)) '(1 2 3))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(2 4 6)"), Times.Once);
    }
}
