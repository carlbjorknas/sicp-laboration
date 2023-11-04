using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class StringTests : EndToEndTestBase
{
    [TestMethod]
    public void A_string_is_self_evaluated()
    {
        SetupInputSequence("\"a_string\"");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("\"a_string\""), Times.Once);
    }

    [TestMethod]
    public void A_string_can_contain_spaces_and_tabs()
    {
        SetupInputSequence("\" \t\"");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("\" \t\""), Times.Once);
    }

    [TestMethod]
    public void A_string_can_contain_escaped_double_quotes()
    {
        SetupInputSequence("\"The \\\"fresh\\\" bread was all dried up.\"");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("\"The \"fresh\" bread was all dried up.\""), Times.Once);
    }

    [TestMethod]
    public void A_string_can_contain_escaped_backslash()
    {
        SetupInputSequence("\"c:\\\\temp\\\\readme.txt\"");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("\"c:\\temp\\readme.txt\""), Times.Once);
    }

    [TestMethod]
    public void The_string_test_method_returns_true_for_a_string()
    {
        SetupInputSequence("(string? \"a_string\")");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("true"), Times.Once);
    }

    [TestMethod]
    public void The_string_test_method_returns_false_for_a_boolean()
    {
        SetupInputSequence("(string? true)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("false"), Times.Once);
    }

    [TestMethod]
    [DataRow("\"\"", 0)]
    [DataRow("\" \"", 1)]
    [DataRow("\"\t\"", 1)]
    [DataRow("\"a string having length \\\"27\\\"\"", 27)]
    public void Can_get_length_of_a_string(string value, int expectedLength)
    {
        SetupInputSequence($"(string-length {value})");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print($"{expectedLength}"), Times.Once);
    }
}
