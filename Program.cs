using System;
using NLog.Web;
using System.IO;
using System.Collections.Generic;

namespace MovieLibrary
{
    class Program
    {
        
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            string movieFilePath = Directory.GetCurrentDirectory() + "\\movies.csv";
            string showFilePath = Directory.GetCurrentDirectory() + "\\shows.csv";
            string videoFilePath = Directory.GetCurrentDirectory() + "\\videos.csv";

            logger.Info("Program started");

            MovieFile movieFile = new MovieFile(movieFilePath);
            ShowFile showFile = new ShowFile(showFilePath);
            VideoFile videoFile = new VideoFile(videoFilePath);

            string choice = "";
            do
            {
                
                Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Add Show");
                Console.WriteLine("3) Add Video");
                Console.WriteLine("4) Display All Movies");
                Console.WriteLine("5) Display All Shows");
                Console.WriteLine("6) Display All Videos");
                Console.WriteLine("Enter to quit");
               
                choice = Console.ReadLine();
                //logger.Info("User choice: {Choice}", choice);

                if (choice == "1")
                {
                    // Add movie
                    Movie movie = new Movie();
                    // ask user to input movie title
                    Console.WriteLine("Enter movie title");
                    // input title
                    movie.title = Console.ReadLine();
                    // verify title is unique
                    if (movieFile.isUniqueTitle(movie.title)){
                        // input genres
                        string input;
                        do
                        {
                            // ask user to enter genre
                            Console.WriteLine("Enter genre (or done to quit)");
                            // input genre
                            input = Console.ReadLine();
                            // if user enters "done"
                            // or does not enter a genre do not add it to list
                            if (input != "done" && input.Length > 0)
                            {
                                movie.genres.Add(input);
                            }
                        } while (input != "done");
                        // specify if no genres are entered
                        if (movie.genres.Count == 0)
                        {
                            movie.genres.Add("(no genres listed)");
                        }
                        // add movie
                        movieFile.AddMovie(movie);
                    }}
                    else if (choice == "2")
                {
                    
                    Show show = new Show();
                    
                    Console.WriteLine("Enter show title");
                    
                    show.title = Console.ReadLine();

                    Console.WriteLine("Enter season");
                    show.season = Console.Read();

                    Console.WriteLine("Enter episode");
                    show.episode = Console.Read();
                    
                    if (showFile.isUniqueTitle(show.title)){
                        string input;
                        do
                        {                            
                            Console.WriteLine("Enter writers (or done to quit)");
                            
                            input = Console.ReadLine();
                           
                            if (input != "done" && input.Length > 0)
                            {
                                show.writers.Add(input);
                            }
                        } while (input != "done");
                        
                        if (show.writers.Count == 0)
                        {
                            show.writers.Add("(no writers listed)");
                        }
                        
                        showFile.AddShow(show);
                    }}
                    if (choice == "3")
                {
                    
                    Video video = new Video();
                    // ask user to input video title
                    Console.WriteLine("Enter video title");
                    // input title
                    video.title = Console.ReadLine();

                    Console.WriteLine("Enter Format");
                    video.format = Console.ReadLine();
                    Console.WriteLine("Enter Length");
                    video.length = Console.Read();
                    Console.WriteLine("Enter Region");
                    
                    // verify title is unique
                    if (videoFile.isUniqueTitle(video.title)){
                        // input regions
                        int inputr;
                        do
                        {
                            // ask user to enter genre
                            Console.WriteLine("Enter region (or 0 to quit)");
                            // input genre
                            inputr = Console.Read();
                            // if user enters "done"
                            // or does not enter a genre do not add it to list
                            if (inputr != 0)
                            {
                                video.regions.Add(inputr);
                            }
                        } while (inputr != 0);
                        // specify if no regions are entered
                        if (video.regions.Count == 0)
                        {
                            video.regions.Add(0);
                        }
                        // add video
                        videoFile.AddVideo(video);
                    }}
                    
                 else if (choice == "4")
                {
                    // Display All Movies
                    foreach(Movie m in movieFile.Movies)
                    {
                        Console.WriteLine(m.Display());
                    }
                }
                else if (choice == "5")
                {
                    // Display All Movies
                    foreach(Show s in showFile.Shows)
                    {
                        Console.WriteLine(s.Display());
                    }
                }
                else if (choice == "6")
                {
                    // Display All Movies
                    foreach(Video v in videoFile.Videos)
                    {
                        Console.WriteLine(v.Display());
                    }
                }
                
            } while (choice == "1" || choice == "2" || choice =="3" || choice == "4" || choice == "5" || choice =="6");

            logger.Info("Program ended");
        }
    }
}
