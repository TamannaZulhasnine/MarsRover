using System;
using System.IO;
using System.Linq;
using MarsRover.Models;
using MarsRover.Controller;
using NLog;

namespace MarsRover
{
    internal class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            
            var rover = new Rover();

            var path = @".\dates.txt";            
            rover.Name = "curiosity";

            Logger.Trace("Application Started");

            // Download Photo by earth date provided by dates in dates.txt file
            if (File.Exists(path))
            {
                var lines = File.ReadLines(@".\dates.txt").ToList();
                foreach (var line in lines)
                {
                    Logger.Trace($"Get the Photos for the date {line}");
                    var manager = new RoverApiManager(new DownloadPhoto());
                    manager.DownloadPhotoForRoverAsync(line, "DEMO_KEY", rover);
                }
            }
            else
            {
                Logger.Warn("dates.txt is missing!");
            }

            //Download photo by Martian sol

           // manager.DownloadPhotoForRoverBySol("1000", "DEMO_KEY", rover);

            Logger.Info("Press any Key to exit");
            Console.ReadLine();
        }
    }
}