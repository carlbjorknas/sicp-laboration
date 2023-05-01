using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SICP;
using SICP.Exceptions;

namespace SICP_Tests;

[TestClass]
public class EvaluatorTests
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
    public void When_expression_is_a_boolean_it_is_returned_as_is()
    {
        var expression = new BooleanExpression(false);
        var result = _sut!.Eval(expression, _env!);
        result.Should().BeSameAs(expression);
    }

    [TestMethod]
    public void When_expression_is_a_number_it_is_returned_as_is()
    {
        var expression = new NumberExpression(123);
        var result = _sut!.Eval(expression, _env!);
        result.Should().BeSameAs(expression);
    }

    [TestMethod]
    public void When_expression_is_a_variable_expression_plus_then_the_primitive_procedure_for_plus_is_returned()
    {
        var expression = new VariableExpression("+");
        var result = _sut!.Eval(expression, _env!);
        result.Should().BeSameAs(PrimitiveProcedurePlus.Instance);
    }

    [TestMethod]
    public void Addition_without_arguments_returns_0()
    {
        var expression = new ProcedureCallExpression(new VariableExpression("+"), null);
        var result = _sut!.Eval(expression, _env!);
        result.Should().BeOfType<NumberExpression>();
        ((NumberExpression)result).Value.Should().Be(0);
    }

    //[TestMethod]
    //public void Addition_with_many_levels_of_expression_nesting()
    //{        
    //    var result = _sut!.Eval("(+ (+ (+ (+ (+ 1 2)))))", _env!);
    //    result.ToString().Should().Be("3");
    //}

    //[TestMethod]
    //public void Subtraction_without_arguments_returns_0()
    //{        
    //    var result = _sut!.Eval("(-)", _env!);
    //    result.ToString().Should().Be("0");
    //}

    //[TestMethod]
    //public void When_given_a_subtraction_with_two_numbers_their_diff_is_returned()
    //{        
    //    var result = _sut!.Eval("(- 2 1)", _env!);
    //    result.ToString().Should().Be("1");
    //}

    //[TestMethod]
    //public void When_given_a_unary_subtraction_of_a_positive_number_it_is_returned_with_a_minus()
    //{        
    //    var result = _sut!.Eval("(- 2)", _env!);
    //    result.ToString().Should().Be("-2");
    //}

    //[TestMethod]
    //public void When_given_a_subtraction_with_three_numbers_the_first_is_reduced_by_the_two_others()
    //{        
    //    var result = _sut!.Eval("(- 10 3 4)", _env!);
    //    result.ToString().Should().Be("3");
    //}

    //[TestMethod]
    //public void Math_operation_with_operands_where_some_are_numbers_and_some_are_expressions()
    //{        
    //    var result = _sut!.Eval("(+ 1 (+ 2 3) (- 4 1) 2)", _env!);
    //    result.ToString().Should().Be((1 + 5 + 3 + 2).ToString());
    //}

    //[TestMethod]
    //public void Can_define_a_variable_with_a_value()
    //{        
    //    var result = _sut!.Eval("(define x 10)", _env!);

    //    result.ToString().Should().Be("ok");
    //    var value = _env!.GetValue("x");
    //    value.IsNumber.Should().BeTrue();
    //    value.ToString().Should().Be("10");
    //}

    //[TestMethod]
    //public void Can_define_a_variable_and_bind_it_to_an_expression()
    //{
    //    var result = _sut!.Eval("(define x (+ 1 1))", _env!);

    //    result.ToString().Should().Be("ok");
    //    var value = _env!.GetValue("x");
    //    value.IsNumber.Should().BeTrue();
    //    value.ToString().Should().Be("2");
    //}

    //[TestMethod]
    //public void A_variable_returns_its_value()
    //{
    //    _sut!.Eval("(define x 10)", _env!);
    //    var result = _sut!.Eval("x", _env!);

    //    result.ToString().Should().Be("10");
    //}

    //[TestMethod]
    //public void A_variable_can_contain_letters_digits_hyphens_and_exclamation_mark()
    //{
    //    _sut!.Eval("(define xyz123-! 10)", _env!);
    //    var result = _sut!.Eval("xyz123-!", _env!);

    //    result.ToString().Should().Be("10");
    //}

    //[TestMethod]
    //public void When_using_an_unbound_variable_an_exception_is_thrown()
    //{
    //    _sut!.Invoking(x => x.Eval("x", _env!))
    //        .Should().Throw<UnboundVariableException>()
    //        .WithMessage("Variable 'x' is unbound.");
    //}

    //[TestMethod]
    //public void Can_use_a_variable_in_an_add_expression()
    //{
    //    _sut!.Eval("(define x 1)", _env!);
    //    var result = _sut!.Eval("(+ 1 x)", _env!);

    //    result.ToString().Should().Be("2");
    //}
}