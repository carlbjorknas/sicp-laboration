using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SICP;
using SICP.Exceptions;
using SICP.Expressions;
using System.Collections.Generic;

namespace SICP_Tests;

[TestClass]
public class EvaluatorTests : TestBase
{
    private Evaluator? _sut;
    private Environment? _env;

    [TestInitialize]
    public void Init()
    {
        _sut = new Evaluator();
        _env = new Environment();
    }

    [TestMethod]
    public void When_expression_is_a_number_it_is_returned_as_is()
    {
        var expression = new NumberExpression(123);
        var result = _sut!.Eval(expression, _env!);
        result.Should().BeSameAs(expression);
    }

    [TestMethod]
    public void A_variable_can_contain_letters_digits_hyphens_and_exclamation_mark()
    {
        var defintionExpression = CreateList(
            new VariableExpression("define"),
            new VariableExpression("xyz123-!"),
            new NumberExpression(10));
        _sut!.Eval(defintionExpression, _env!);

        var result = _sut!.Eval(new VariableExpression("xyz123-!"), _env!);

        result.ToString().Should().Be("10");
    }

    [TestMethod]
    public void Can_use_a_variable_in_an_add_expression()
    {
        var defintionExpression = CreateList(
            new VariableExpression("define"), 
            new VariableExpression("x"), 
            new NumberExpression(1));
        _sut!.Eval(defintionExpression, _env!);

        var addExpression = CreateList(
            new VariableExpression("+"),
            new NumberExpression(1),
            new VariableExpression("x"));
        var result = _sut!.Eval(addExpression, _env!);

        result.ToString().Should().Be("2");
    }
}