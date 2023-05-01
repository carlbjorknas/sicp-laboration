﻿using FluentAssertions;
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

        value.Should().BeSameAs(PrimitiveProcedurePlus.Instance);
    }

    //[TestMethod]
    //public void A_new_Environment_contains_the_primitive_procedure_minus()
    //{
    //    var env = new Environment();
    //    var value = env.GetValue("-");

    //    value.IsPrimitiveProcedure.Should().BeTrue();
    //    value.ToString().Should().Be("-");
    //}
}
