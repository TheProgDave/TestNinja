using System;
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
        [Test]
        public void Add_WhenCalled_ReturnSummedArguments()
        {
            var math = new Math();

            var result = math.Add(1, 2);

            Assert.That(result.Equals(3));
        }

        [Test]
        public void Max_ArgumentsDiffer_ReturnLargerArgument()
        {
            var math = new Math();

            var result = math.Max(1, 2);

            Assert.That(result.Equals(2));
        }

        [Test]
        public void Max_ArgumentsSame_ReturnLargerArgument()
        {
            var math = new Math();

            var result = math.Max(1, 1);

            Assert.That(result.Equals(1));
        }
    }
}
