using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class MultiplicationTests : EndToEndTestBase
{
    [TestMethod]
    public void Multiplication_without_operands_returns_1()
    {
        SetupInputSequence("(*)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("1"), Times.Once);
    }

    [TestMethod]
    public void Multiplication_with_a_single_operand_returns_the_operand()
    {
        SetupInputSequence("(* 2)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("2"), Times.Once);
    }

    [TestMethod]
    public void Multiplication_of_2_3_and_4_returns_24()
    {
        SetupInputSequence("(* 2 3 4)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("24"), Times.Once);
    }
}
