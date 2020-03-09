using System;
using Moq;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{

    [TestFixture]
    public class VideoServiceUnitTests
    {
        [SetUp]
        public void SetUp()
        {
            var fileReader = new Mock<IFileReader>());
        }
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            
        }
    }
    
    
    [TestFixture]
    public class DemeritPointsCalculatorUnitTests
    {
        private DemeritPointsCalculator _demeritsCalculator;

        [SetUp]
        public void SetUp()
        {
            _demeritsCalculator = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(69,0)]
        [TestCase(70,1)]
        [TestCase(300, 47)]
        [TestCase(0,0)]
        public void CalculateDemeritPoints_ValidSpeed_ReturnsDemeritPoints(int speed, int expected)
        {
            Assert.That(_demeritsCalculator.CalculateDemeritPoints(speed), Is.EqualTo(expected));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        public void CalculateDemeritPoints_SpeedOutOfRange_ThrowsArgumentOutOfRangeException(int number)
        {
            Assert.That(()=>_demeritsCalculator.CalculateDemeritPoints(number), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}