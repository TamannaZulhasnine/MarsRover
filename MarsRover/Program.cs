﻿using System;
using System.IO;
using System.Linq;
using NLog;

namespace MarsRover
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            Logger.Trace("Application Started");
            string path = @".\dates.txt";
            
            MarsRoverApiManager manager = new MarsRoverApiManager();

            if (File.Exists(path))
            {
                var lines = File.ReadLines(@".\dates.txt").ToList();
                foreach (var line in lines)
                {
                    Logger.Trace($"Get the Photos for the date {line}");
                    manager.GetPhotosAsync(line, "DEMO_KEY");
                }
            }
            else
            {
                Logger.Warn("dates.txt is missing!");
            }

            Logger.Info("Press any Key to exit");
            Console.ReadLine();
        }
    }
}
