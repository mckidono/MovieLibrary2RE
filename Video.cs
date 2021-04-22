using System;
using System.Collections.Generic;

namespace MovieLibrary
{
    public class Video
    {
        public UInt64 videoId { get; set; }
        // private field
        string _title;
        public string title
        {
            get
            {
                return this._title;
            }
            set
            {
                // if there is a comma(,) in the title, wrap it in quotes
                this._title = value.IndexOf(',') != -1 ? $"\"{value}\"" : value;
            }
        }
        public List<int> regions { get; set; }
        public string format{get; set;}
        public int length{get; set;}

        // constructor
        public Video()
        {
            regions = new List<int>();
        }

        public string Display()
        {
            return $"Id: {videoId}\nTitle: {title}\nregions: {string.Join(", ", regions)}\n";
        }
    }
}
