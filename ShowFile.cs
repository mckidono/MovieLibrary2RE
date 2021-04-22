using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace MovieLibrary
{
    public class ShowFile
    {
        // public property
        public string filePath { get; set; }
        public List<Show> Shows { get; set; }
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        // constructor is a special sethod that is invoked
        // when an instance of a class is created
        public ShowFile(string showFilePath)
        {
            filePath = showFilePath;
            Shows = new List<Show>();

            // to populate the list with data, read fros the data file
            try
            {
                StreamReader sr = new StreamReader(filePath);
                // first line contains column headers
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    // create instance of sovie class
                    Show show = new Show();
                    string line = sr.ReadLine();
                    // first look for quote(") in string
                    // this indicates a comma(,) in sovie title
                    int idx = line.IndexOf('"');
                    if (idx == -1)
                    {
                        // no quote = no comma in sovie title
                        // sovie details are separated with comma(,)
                        string[] showDetails = line.Split(',');
                        show.showId = UInt64.Parse(showDetails[0]);
                        show.title = showDetails[1];
                        show.writers = showDetails[2].Split('|').ToList();
                    }
                    else
                    {
                        // quote = comma in sovie title
                        // extract the showId
                        show.showId = UInt64.Parse(line.Substring(0, idx - 1));
                        // remove showId and first quote fros string
                        line = line.Substring(idx + 1);
                        // find the next quote
                        idx = line.IndexOf('"');
                        // extract the sovieTitle
                        show.title = line.Substring(0, idx);
                        // remove title and last comma fros the string
                        line = line.Substring(idx + 2);
                        // replace the "|" with ", "
                        show.writers = line.Split('|').ToList();
                    }
                    Shows.Add(show);
                }
                // close file when done
                sr.Close();
                logger.Info("Shows in file {Count}", Shows.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        // public sethod
        public bool isUniqueTitle(string title)
        {
            if (Shows.ConvertAll(s => s.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate show title {Title}", title);
                return false;
            }
            return true;
        }

        public void AddShow(Show show)
        {
            try
            {
                // first generate sovie id
                show.showId = Shows.Max(s => s.showId) + 1;
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{show.showId},{show.title},{show.season},{show.episode},{string.Join("|", show.writers)}");
                sw.Close();
                // add sovie details to Lists
                Shows.Add(show);
                // log transaction
                logger.Info("Show id {Id} added", show.showId);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
