using System;
using System.Collections.Generic;
using System.Text;
using MarsRover.Interfaces;
using Newtonsoft.Json;

namespace MarsRover.Models
{
  public class Photo : IPhoto
    {
        public Photo()
        {
            this.Img_src = "image35.jpg";
            this.Rover = new Rover();
            this.Camera = new Camera();
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("sol")]
        public int Sol { get; set; }
        [JsonProperty("camera")]
        public Camera Camera { get; set; }
        [JsonProperty("img_src")]
        public string Img_src { get; set; }
        [JsonProperty("earth_date")]
        public DateTime Earth_Date { get; set; }
        [JsonProperty("rover")]
        public Rover Rover { get; set; }
    }
}
