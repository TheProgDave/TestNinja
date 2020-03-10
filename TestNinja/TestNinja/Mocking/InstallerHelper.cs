using System.Net;

namespace TestNinja.Mocking
{
    public interface IFileDownloader
    {
        void DownloadFile(string url, string path);
    }

    public class FileDownloader : IFileDownloader
    {
        public void DownloadFile(string url, string path)
        {
            WebClient client = new WebClient();
            client.DownloadFile(url, path );
        }
    }
    
    public class InstallerHelper
    {
        private string _setupDestinationFile;
        private readonly IFileDownloader _downloader;

        public InstallerHelper(IFileDownloader downloader = null)
        {
            _downloader = downloader ?? new FileDownloader();
        }

        public bool DownloadInstaller(string customerName, string installerName)
        { 
            try
            {
                _downloader.DownloadFile($"http://example.com/{customerName}/{installerName}", _setupDestinationFile);
                return true;
            }
            catch (WebException)
            {
                return false; 
            }
        }
    }
}