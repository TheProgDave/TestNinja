using System;
using System.Runtime.Remoting.Channels;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ErrorLoggerUnitTests
    {
        private ErrorLogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new ErrorLogger();
        }

        [Test]
        [TestCase("a")]
        public void Log_WhenCalled_SetTheLastErrorProperty(string input)
        {
            _logger.Log(input);
            Assert.That(_logger.LastError, Is.EqualTo(input));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowsArgumentNullException(string error)
        {
            Assert.That(() => _logger.Log(error), Throws.ArgumentNullException);
        }

        [Test]
        public void Log_ValidError_RaiseErrorLogEvent()
        {
            var id = Guid.Empty;
            _logger.ErrorLogged += (sender, args) => { id = args; };
            _logger.Log("a");
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }

    }
}