using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class BigIntegerTests : EndToEndTestBase
{
    [TestMethod]
    public void Multiplication_does_not_overflow_for_large_products()
    {
        SetupInputSequence("(* 1000000 1000000 1000000)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("1000000000000000000"), Times.Once);
    }

    [TestMethod]
    public void A_large_integer_literal_is_parsed_and_used()
    {
        SetupInputSequence("(+ 100000000000000000000 1)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("100000000000000000001"), Times.Once);
    }

    [TestMethod]
    public void Factorial_of_25_is_exact()
    {
        SetupInputSequence(
            "(define (factorial n) (if (= n 1) 1 (* n (factorial (- n 1)))))",
            "(factorial 25)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("15511210043330985984000000"), Times.Once);
    }

    [TestMethod]
    public void A_malformed_number_literal_is_rejected()
    {
        SetupInputSequence("(+ 12x3 1)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("'12x3' is not a valid number."), Times.Once);
    }
}
