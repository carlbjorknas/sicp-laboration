using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SICP;

namespace SICP_Tests;

[TestClass]
public class EnvironmentTests
{
    [TestMethod]
    public void A_new_Environment_contains_the_primitive_procedure_plus()
    {
        var env = new Environment();
        var value = env.GetValue("+");
        value.Should().BeOfType<PrimitiveProcedurePlus>();
    }

    [TestMethod]
    public void A_new_Environment_contains_the_primitive_procedure_minus()
    {
        var env = new Environment();
        var value = env.GetValue("-");
        value.Should().BeOfType<PrimitiveProcedureMinus>();
    }
}
