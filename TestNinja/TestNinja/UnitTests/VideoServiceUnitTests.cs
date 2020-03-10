using System.Collections.Generic;
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
        private Mock<IVideoRepository> _videoRepository;

        [SetUp]
        public void SetUp()
        { 
            _fileReader = new Mock<IFileReader>();
            _videoRepository = new Mock<IVideoRepository>();
            _service = new VideoService(_fileReader.Object, _videoRepository.Object);
        }
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");
            Assert.That(_service.ReadVideoTitle(), Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_UnprocessedVideos_ReturnsCommaDelimitedVideoTitles()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>
            {
                new Video {Id = 1},
                new Video {Id = 2},
                new Video {Id = 3}
            });

            Assert.That(_service.GetUnprocessedVideosAsCsv(), Is.EqualTo("1,2,3"));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosProcessed_ReturnsCommaDelimitedVideoTitles()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            Assert.That(_service.GetUnprocessedVideosAsCsv(), Is.EqualTo(""));
        }
    }
}