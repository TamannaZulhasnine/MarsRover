using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebClient.ViewModel
{
    public class RoverPhotosViewModel
    {
        public List<string> ListDate { get; set; }
        public string SelectedDate { get; set; }
        public IEnumerable<SelectListItem> Date { get; set; }

       
    }
}
