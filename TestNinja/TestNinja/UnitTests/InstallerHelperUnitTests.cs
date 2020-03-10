using System.Net;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class InstallerHelperUnitTests
    {
        private Mock<FileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<FileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }
        
        [Test]
        public void DownloadFile_InvalidFilePath_ReturnsFalse()
        {
            _fileDownloader.Setup(f => f.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>(); // should fail if the Download file method cannot find paths

            var result = _installerHelper.DownloadInstaller("customer", "installer");
            
            Assert.That(result, Is.False);
        }


        [Test]
        public void DownloadFile_ValidFilePath_ReturnsTrue()
        {
            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.True);
        }
    }
}