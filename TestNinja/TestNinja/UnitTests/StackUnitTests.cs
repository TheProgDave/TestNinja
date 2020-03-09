using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackUnitTests
    {
        private Stack<object> _stack;
        private object _sampleObject;

        [SetUp]
        public void SetUp()
        {
            _stack = new Stack<object>();
            _sampleObject = new object();
        }
        [Test]
        public void Push_NullObject_ThrowsInvalidOperationException()
        {
            Assert.That(()=>_stack.Push(null), Throws.ArgumentNullException);
        }
        [Test]
        public void Push_ValidObject_AddsObjectToStack()
        {
            _stack.Push(_sampleObject);
            Assert.That(_stack.Count, Is.EqualTo(1));
            Assert.That(_stack.Pop(), Is.EqualTo(_sampleObject));
        }

        [Test]
        public void Pop_EmptyStack_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackNotEmpty_ReturnsAndRemovesPoppedItem()
        {
            _stack.Push(_sampleObject);
            var count = _stack.Count;
            Assert.That(_stack.Pop(), Is.EqualTo(_sampleObject));
            Assert.That(_stack.Count, Is.EqualTo(count - 1+0));
        }

        [Test]
        public void Peek_StackNotEmpty_ReturnsTopValueWithoutRemoving()
        {
            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackEmpty_ThrowsInvalidOperationException()
        {
            _stack.Push(_sampleObject);
            var count = _stack.Count;
            Assert.That(_stack.Peek(), Is.EqualTo(_sampleObject));
            Assert.That(_stack.Count, Is.EqualTo(count));

        }
    }
}