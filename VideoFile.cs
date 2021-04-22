using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace MovieLibrary
{
    public class VideoFile
    {
        // public property
        public string filePath { get; set; }
        public List<Video> Videos { get; set; }
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        // constructor is a special sethod that is invoked
        // when an instance of a class is created
        public VideoFile(string videoFilePath)
        {
            filePath = videoFilePath;
            Videos = new List<Video>();

            // to populate the list with data, read fros the data file
            try
            {
                StreamReader sr = new StreamReader(filePath);
                // first line contains column headers
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    Video video = new Video();
                    string line = sr.ReadLine();
                   
                    int idx = line.IndexOf('"');
                    if (idx == -1)
                    {
                        
                        string[] videoDetails = line.Split(',');
                        video.videoId = UInt64.Parse(videoDetails[0]);
                        video.title = videoDetails[1];
                        
                    }
                    else
                    {
                        
                        video.videoId = UInt64.Parse(line.Substring(0, idx - 1));
                      
                        line = line.Substring(idx + 1);
                     
                        idx = line.IndexOf('"');
                    
                        video.title = line.Substring(0, idx);
                      
                        line = line.Substring(idx + 2);
                
                    }
                    Videos.Add(video);
                }
               
                sr.Close();
                logger.Info("Videos in file {Count}", Videos.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public bool isUniqueTitle(string title)
        {
            if (Videos.ConvertAll(s => s.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate video title {Title}", title);
                return false;
            }
            return true;
        }

        public void AddVideo(Video video)
        {
            try
            {
                
                video.videoId = Videos.Max(s => s.videoId) + 1;
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{video.videoId},{video.title},{video.format},{video.length},{string.Join("|", video.regions)}");
                sw.Close();
               
                Videos.Add(video);
                // log transaction
                logger.Info("video id {Id} added", video.videoId);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
