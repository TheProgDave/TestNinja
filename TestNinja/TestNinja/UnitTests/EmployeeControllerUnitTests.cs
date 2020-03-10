using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class EmployeeControllerUnitTests
    {
        private EmployeeController _controller;
        private Mock<EmployeeStorage> _storage;

        [SetUp]
        public void SetUp()
        {
            
            _storage = new Mock<EmployeeStorage>();
            _controller = new EmployeeController(_storage.Object);
        }
    }
}