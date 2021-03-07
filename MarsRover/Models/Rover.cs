using Newtonsoft.Json;

namespace MarsRover.Models
{
    public class Rover : IRover
    {
        public Rover()
        {
            this.Id = this.Id;
            this.Name = this.Name;
        }

        [JsonProperty("landing_date")] public string Landing_Date { get; set; }

        [JsonProperty("launching_date")] public string Launching_Date { get; set; }

        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }
}