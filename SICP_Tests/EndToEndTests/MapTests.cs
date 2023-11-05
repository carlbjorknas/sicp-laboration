using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class MapTests : EndToEndTestBase
{
    [TestMethod]
    public void When_mapping_over_the_empty_list_the_empty_list_is_returned()
    {
        SetupInputSequence("(map (lambda (x) (* x 2)) '())");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("()"), Times.Once);
    }

    [TestMethod]
    public void Can_map_over_a_single_list()
    {
        SetupInputSequence("(map (lambda (x) (* x 2)) '(1 2 3))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(2 4 6)"), Times.Once);
    }

    [TestMethod]
    public void Can_map_over_multiple_lists()
    {
        SetupInputSequence("(map + '(1 1 1) '(1 2 3) '(2 3 4))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(4 6 8)"), Times.Once);
    }

    [TestMethod]
    public void Can_map_over_multiple_lists_with_different_lengths()
    {
        SetupInputSequence("(map + '(1 1 1 1 1) '(1 2 3))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(2 3 4)"), Times.Once);
    }
}
