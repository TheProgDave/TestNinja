using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzUnitTests
    {
        [Test]
        [TestCase(15, "FizzBuzz")]
        [TestCase(5, "Buzz")]
        [TestCase(3, "Fizz")]
        [TestCase(1,"1")]
        public void GetOutput_NumberInputs_ReturnsCorrectFizzBuzz(int input,string output)
        {
            Assert.That(FizzBuzz.GetOutput(input), Is.EqualTo(output));
        }


    }
}