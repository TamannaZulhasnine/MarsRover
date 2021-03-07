using System;
using MarsRover.Interfaces;
using Newtonsoft.Json;

namespace MarsRover.Models
{
    public class Camera : ICamera
    {
        public Camera()
        {
            this.Full_Name = String.Empty;
            this.Name = String.Empty;
        }

        [JsonProperty("full_name")] public string Full_Name { get; set; }

        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("rover_id")] public int Rover_Id { get; set; }
    }
}