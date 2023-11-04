using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class MinusTests : EndToEndTestBase
{
    [TestMethod]
    public void Subtraction_without_arguments_returns_0()
    {
        SetupInputSequence("(-)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("0"), Times.Once);
    }

    [TestMethod]
    public void When_given_a_unary_subtraction_of_a_positive_number_it_is_returned_with_a_minus()
    {
        SetupInputSequence("(- 2)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("-2"), Times.Once);
    }

    [TestMethod]
    public void When_given_a_subtraction_with_two_numbers_their_diff_is_returned()
    {
        SetupInputSequence("(- 2 1)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("1"), Times.Once);
    }

    [TestMethod]
    public void When_given_a_subtraction_with_three_numbers_the_first_is_reduced_by_the_two_others()
    {
        SetupInputSequence("(- 10 3 4)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("3"), Times.Once);
    }
}
