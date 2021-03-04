using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MarsRover.Models
{
 public class Rover : IRover
    {
      public Rover()
        {
            this.Id = Id;
            this.Name = Name;
        }
        
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("landing_date")]
        public string Landing_Date { get; set; }
        [JsonProperty("launching_date")]
        public string Launching_Date { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
