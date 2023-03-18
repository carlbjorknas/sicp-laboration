using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SICP;

namespace SICP_Tests
{
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
        public void When_given_an_addition_with_two_numbers_their_sum_is_returned()
        {
            var sut = new Engine();
            var result = sut.Eval("(+ 1 2)");
            result.Should().Be("3");
        }
    }
}