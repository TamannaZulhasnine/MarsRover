using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using MarsRover;
using System.Threading.Tasks;
using MarsRover.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsRoverTest
{
    [TestClass]
    public class MarsRoverApiTest
    {
        string baseString = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/";
        string date = "2015-6-3";
        string apiKey = "DEMO_KEY";
                
        private DownloadPhoto downloadPhoto = new DownloadPhoto();

        [TestMethod]
        public void GetDateTest()
        {
            var datestring = "June 2, 2018";
            var datestringFormat = "2018-06-02"; 
            var date = downloadPhoto.GetDate(datestring);
            Assert.AreEqual(date,datestringFormat);
        }

        [TestMethod]
        public void GetUriTest()
        {
            var uriExpected = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=2015-6-3&api_key=DEMO_KEY";
            var UriPath = downloadPhoto.GetUriPath(baseString, date, apiKey);
            var UriActual = Uri.UnescapeDataString(UriPath.ToString());
            Assert.AreEqual(uriExpected, UriActual);
        }

        [TestMethod]
        public void Download_WillReturn_FilePath()
        {
            string testdate = "2017-02-27";
            downloadPhoto.DownloadPhotoAsync(baseString, testdate, apiKey).Wait();
            
            var expectedPath = @"c:\MarsRover\2017-02-27\";
            
            Assert.IsTrue(Directory.Exists(expectedPath));

            string[] fileEntries = Directory.GetFiles(expectedPath);
            foreach (string fileName in fileEntries)
            {
                Assert.IsTrue(fileName.EndsWith(".jpg"));
            }
                

            
        }
    }
}
