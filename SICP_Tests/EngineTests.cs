using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SICP;

namespace SICP_Tests;

[TestClass]
public class EngineTests
{
    [TestMethod]
    public void When_given_a_number_the_number_is_returned()
    {
        var sut = new Engine();
        var result = sut.Eval("6");
        result.Should().Be("6");
    }

    [TestMethod]
    public void Addition_without_arguments_returns_0()
    {
        var sut = new Engine();
        var result = sut.Eval("(+)");
        result.Should().Be("0");
    }

    [TestMethod]
    public void When_given_an_addition_with_two_numbers_their_sum_is_returned()
    {
        var sut = new Engine();
        var result = sut.Eval("(+ 1 2)");
        result.Should().Be("3");
    }

    [TestMethod]
    public void Addition_where_the_only_operand_is_an_addition_expression()
    {
        var sut = new Engine();
        var result = sut.Eval("(+ (+ 1 2))");
        result.Should().Be("3");
    }

    [TestMethod]
    public void Addition_with_many_levels_of_expression_nesting()
    {
        var sut = new Engine();
        var result = sut.Eval("(+ (+ (+ (+ (+ 1 2)))))");
        result.Should().Be("3");
    }

    [TestMethod]
    public void Subtraction_without_arguments_returns_0()
    {
        var sut = new Engine();
        var result = sut.Eval("(-)");
        result.Should().Be("0");
    }

    [TestMethod]
    public void When_given_a_subtraction_with_two_numbers_their_diff_is_returned()
    {
        var sut = new Engine();
        var result = sut.Eval("(- 2 1)");
        result.Should().Be("1");
    }

    [TestMethod]
    public void When_given_a_unary_subtraction_of_a_positive_number_it_is_returned_with_a_minus()
    {
        var sut = new Engine();
        var result = sut.Eval("(- 2)");
        result.Should().Be("-2");
    }

    [TestMethod]
    public void When_given_a_subtraction_with_three_numbers_the_first_is_reduced_by_the_two_others()
    {
        var sut = new Engine();
        var result = sut.Eval("(- 10 3 4)");
        result.Should().Be("3");
    }

    [TestMethod]
    public void Math_operation_with_operands_where_some_are_numbers_and_some_are_expressions()
    {
        var sut = new Engine();
        var result = sut.Eval("(+ 1 (+ 2 3) (- 4 1) 2)");
        result.Should().Be((1 + 5 + 3 + 2).ToString());
    }
}