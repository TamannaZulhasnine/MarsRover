using System;
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

        [JsonProperty("sol")] public int Sol { get; set; }

        [JsonProperty("img_src")] public string Img_src { get; set; }

        [JsonProperty("earth_date")] public DateTime Earth_Date { get; set; }

        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("camera")] public Camera Camera { get; set; }

        [JsonProperty("rover")] public Rover Rover { get; set; }
    }
}