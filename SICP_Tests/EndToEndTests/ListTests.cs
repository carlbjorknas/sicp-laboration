using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class ListTests : EndToEndTestBase
{
    [TestMethod]
    public void Empty_list_evaluates_to_itself()
    {
        SetupInputSequence($"()");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("()"), Times.Once);
    }

    [TestMethod]
    public void Cons_a_number_with_empty_list()
    {
        SetupInputSequence($"(cons 1 ())");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(1)"), Times.Once);
    }

    [TestMethod]
    public void Cons_a_number_two_times()
    {
        SetupInputSequence($"(cons 1 (cons 2 ()))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(1 2)"), Times.Once);
    }

    [TestMethod]
    public void Create_a_dotted_pair()
    {
        SetupInputSequence($"(cons 1 2)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(1 . 2)"), Times.Once);
    }

    [TestMethod]
    public void A_list_within_a_list()
    {
        SetupInputSequence("(cons (cons 1 (cons 2 ())) (cons 3 ()))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("((1 2) 3)"), Times.Once);
    }

    [TestMethod]
    public void Calling_List_without_arguments_create_empty_list()
    {
        SetupInputSequence("(list)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("()"), Times.Once);
    }

    [TestMethod]
    public void Calling_List_with_quoted_symbols_creates_a_list_with_the_symbols()
    {
        SetupInputSequence("(list 'a 'b 'c)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(a b c)"), Times.Once);
    }

    [TestMethod]
    public void Calling_List_with_subexpressions()
    {
        SetupInputSequence("(list (+ 1 1) (- 1 1))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(2 0)"), Times.Once);
    }

    [TestMethod]
    public void Append_without_arguments_return_empty_list()
    {
        SetupInputSequence("(append)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("()"), Times.Once);
    }

    [TestMethod]
    public void Append_with_single_argument_returns_the_argument()
    {
        SetupInputSequence("(append 2)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("2"), Times.Once);
    }

    [TestMethod]
    public void Can_append_two_lists()
    {
        SetupInputSequence("(append '(a b) '(c d))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(a b c d)"), Times.Once);
    }

    [TestMethod]
    public void Append_handles_single_empty_list()
    {
        SetupInputSequence("(append '(a b) '() '(c d))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(a b c d)"), Times.Once);
    }

    [TestMethod]
    public void Append_handles_double_empty_list()
    {
        SetupInputSequence("(append '(a b) '() '() '(c d))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(a b c d)"), Times.Once);
    }

    [TestMethod]
    public void Append_can_create_dotted_pair()
    {
        SetupInputSequence("(append '(a b) 'c)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(a b . c)"), Times.Once);
    }

    [TestMethod]
    public void Append_requires_all_but_the_last_argument_to_be_of_type_list()
    {
        SetupInputSequence("(append 1 2)");
        _sut!.Run();
        _printerMock!.Verify(
            x => x.Print("The method 'append' requires all but the last argument to be of type 'list'."),
            Times.Once);
    }

    [TestMethod]
    public void Append_ignores_the_cdr_of_a_dotted_pair()
    {
        SetupInputSequence("(append (cons 'a 'b) '(c))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(a c)"), Times.Once);
    }

    [TestMethod]
    public void Car_returns_the_first_object_in_a_list()
    {
        SetupInputSequence("(car '(1 2))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("1"), Times.Once);
    }

    [TestMethod]
    public void Cdr_returns_the_rest_of_a_list()
    {
        SetupInputSequence("(cdr '(1 2))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("(2)"), Times.Once);
    }

    [TestMethod]
    public void Can_car_a_list_returned_from_cdr()
    {
        SetupInputSequence("(car (cdr '(1 2)))");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("2"), Times.Once);
    }
}
