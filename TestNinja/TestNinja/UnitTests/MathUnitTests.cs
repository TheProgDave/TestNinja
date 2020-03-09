using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestNinja.Fundamentals;
using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class MathUnitTests
    {
        private Math _math;

        [SetUp]
        public void Setup()
        {
            _math = new Math();
        }

        // Parameterized test methods:
        [Test]
        [Ignore("I felt like Ignoring this test!")]
        [TestCase(2,1,2)]
        [TestCase(1, 2, 2)]
        [TestCase(2, 2, 2)]
        public void Max_WhenCalled_ReturnsLargestArgument(int x, int y, int expected)
        {
            var result = _math.Max(x, y);

            Assert.That(result.Equals(expected));
        }

        [Test]
        public void Add_WhenCalled_ReturnSummedArguments()
        {
            var result = _math.Add(1, 2);
            Assert.That(result.Equals(3));
        }

        [Test]
        public void Max_FirstArgumentIsGreater_ReturnFirstArgument()
        {
            var result = _math.Max(2, 1);
            Assert.That(result.Equals(2));
        }

        [Test]
        public void Max_SecondArgumentIsGreater_ReturnSecondArgument()
        {
            var result = _math.Max(2, 1);
            Assert.That(result.Equals(2));
        }


        [Test]
        public void Max_ArgumentsSame_ReturnLargerArgument()
        {
            var result = _math.Max(1, 1);
            Assert.That(result.Equals(1));
        }


        [Test]
        public void GetOddNumbers_LimitGreaterThanZero_ReturnOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(3));
            

            Assert.That(result, Does.Contain(1));
            Assert.That(result, Does.Contain(1)); 
            Assert.That(result, Does.Contain(1));

            // checks if all elements are present - unordered - simplifies the previous 3 lines
            Assert.That(result, Is.EquivalentTo(new int[] {1,3,5})); 


        }
    }
}
