using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class VideoServiceUnitTests
    {
        private Mock<IFileReader> _fileReader;
        private VideoService _service;
        private Mock<VideoService> _videoContext;

        [SetUp]
        public void SetUp()
        { 
            _fileReader = new Mock<IFileReader>();
            _service = new VideoService(_fileReader.Object);
            _videoContext = new Mock<VideoService>();
        }
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");
            Assert.That(_service.ReadVideoTitle(), Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_HasUnprocessedVideos_ReturnsCommaDelimitedVideoTitles()
        {
            
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_NoUnprocessedVideos_ReturnsCommaDelimitedVideoTitles()
        {

        }
    }
}