using System;
using System.Collections.Generic;

namespace MovieLibrary
{
    public class Show
    {
        public UInt64 showId { get; set; }
        
        string _title;
        public string title
        {
            get
            {
                return this._title;
            }
            set
            {
                
                this._title = value.IndexOf(',') != -1 ? $"\"{value}\"" : value;
            }
        }
        public List<string> writers { get; set; }
        public int season{get; set;}
        public int episode{get; set;}

        
        public Show()
        {
            writers = new List<string>();
        }

        public string Display()
        {
            return $"Id: {showId}\nTitle: {title}\nSeason: {season}\nEpisode: {episode}\nWriters: {string.Join(", ", writers)}\n";
        }
    }
}
