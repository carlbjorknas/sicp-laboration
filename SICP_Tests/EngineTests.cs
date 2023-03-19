using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SICP;

namespace SICP_Tests;

[TestClass]
public class EngineTests
{
    private Engine _sut;
    private Environment _env;

    [TestInitialize]
    public void Init()
    {
        _sut = new Engine();
        _env = new Environment();
    }

    [TestMethod]
    public void When_given_a_number_the_number_is_returned()
    {        
        var result = _sut.Eval("6", _env);
        result.Should().Be("6");
    }

    [TestMethod]
    public void Addition_without_arguments_returns_0()
    {        
        var result = _sut.Eval("(+)", _env);
        result.Should().Be("0");
    }

    [TestMethod]
    public void When_given_an_addition_with_two_numbers_their_sum_is_returned()
    {        
        var result = _sut.Eval("(+ 1 2)", _env);
        result.Should().Be("3");
    }

    [TestMethod]
    public void Addition_where_the_only_operand_is_an_addition_expression()
    {        
        var result = _sut.Eval("(+ (+ 1 2))", _env);
        result.Should().Be("3");
    }

    [TestMethod]
    public void Addition_with_many_levels_of_expression_nesting()
    {        
        var result = _sut.Eval("(+ (+ (+ (+ (+ 1 2)))))", _env);
        result.Should().Be("3");
    }

    [TestMethod]
    public void Subtraction_without_arguments_returns_0()
    {        
        var result = _sut.Eval("(-)", _env);
        result.Should().Be("0");
    }

    [TestMethod]
    public void When_given_a_subtraction_with_two_numbers_their_diff_is_returned()
    {        
        var result = _sut.Eval("(- 2 1)", _env);
        result.Should().Be("1");
    }

    [TestMethod]
    public void When_given_a_unary_subtraction_of_a_positive_number_it_is_returned_with_a_minus()
    {        
        var result = _sut.Eval("(- 2)", _env);
        result.Should().Be("-2");
    }

    [TestMethod]
    public void When_given_a_subtraction_with_three_numbers_the_first_is_reduced_by_the_two_others()
    {        
        var result = _sut.Eval("(- 10 3 4)", _env);
        result.Should().Be("3");
    }

    [TestMethod]
    public void Math_operation_with_operands_where_some_are_numbers_and_some_are_expressions()
    {        
        var result = _sut.Eval("(+ 1 (+ 2 3) (- 4 1) 2)", _env);
        result.Should().Be((1 + 5 + 3 + 2).ToString());
    }

    [TestMethod]
    public void Can_define_a_variable_with_a_value()
    {        
        var result = _sut.Eval("(define x 10)", _env);

        result.Should().Be("ok");
        _env.GetValue("x").Should().Be("10");
    }
}