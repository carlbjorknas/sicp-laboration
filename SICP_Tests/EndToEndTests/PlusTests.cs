using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class PlusTests : EndToEndTestBase
{
    [TestMethod]
    public void When_entering_a_plus_then_the_string_PrimitiveProcedure_is_printed()
    {
        SetupInputSequence("+");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("PrimitiveProcedure"), Times.Once);
    }

    [TestMethod]
    public void When_calling_plus_without_operands_then_zero_is_printed()
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
}
