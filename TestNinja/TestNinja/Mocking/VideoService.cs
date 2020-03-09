using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public interface IFileReader
    {
        string Read(string path);
    }

    public class FileReader : IFileReader
    {
        public string Read(string path)
        {
            return File.ReadAllText(path);
        }
    }
    
    public class VideoService
    {
        private IFileReader _fileReader;
        private IVideoRepository _videoService;

        public VideoService(IFileReader reader = null, IVideoRepository service = null)
        {
            _fileReader = reader ?? new FileReader();
            _videoService = service ?? new VideoRepository();
        }
        
        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt"); // test here!
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = _videoService.GetUnprocessedVideos().Select(v => v.Id).ToList();
            return String.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }
}