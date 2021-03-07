using System;
using System.Runtime.InteropServices.ComTypes;
using MarsRover;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsRoverTest
{
    [TestClass]
    public class MarsRoverApiTest
    {
        private MarsRoverApiManager apiManager = new MarsRoverApiManager();
        
        [TestMethod]
        public void GetDateTest()
        {
            var datestring = "June 2, 2018";
            var datestringFormat = "2018-06-02"; 
            var date = apiManager.GetDate(datestring);
            Assert.AreEqual(date,datestringFormat);
        }

        [TestMethod]
        public void GetUriTest()
        {
            var baseString = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/";
            var date = "2015-6-3";
            var apiKey = "DEMO_KEY";
            var uriExpected = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=2015-6-3&api_key=DEMO_KEY";
            var UriPath = apiManager.GetUriPath(baseString, date, apiKey);
            var UriActual = Uri.UnescapeDataString(UriPath.ToString());
            Assert.AreEqual(uriExpected, UriActual);
        }
    }
}
