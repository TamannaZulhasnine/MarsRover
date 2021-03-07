using System;

namespace WebClient.Models
{
    public class Photo
    {
        public int Id { get; set; }

        public int Sol { get; set; }

        public Camera Camera { get; set; }

        public string Img_src { get; set; }

        public DateTime Earth_Date { get; set; }
    }
}