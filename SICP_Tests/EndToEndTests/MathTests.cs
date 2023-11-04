using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

public class MathTests : EndToEndTestBase
{
    [TestMethod]
    public void Math_operation_with_operands_where_some_are_numbers_and_some_are_expressions()
    {
        SetupInputSequence("(+ 1 (+ 2 3) (- 4 1) 2)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print((1 + 2 + 3 + (4 - 1) + 2).ToString()), Times.Once);
    }
}
