using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Wine
    {
        [Required()]
        public string Name { get; set; }
        [Required()]
        public string Style { get; set; }
        [Required()]
        public int WineryId { get; set; }
        [Required()]
        public string Description { get; set; }
        public int WineId { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }
    }
}
