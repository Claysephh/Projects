using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Winery
    {
        public int WineryId { get; set; }
        [Required()]
        public string WineryName { get; set; }
        [Required()]
        public string WineryCountry { get; set; }
        [Required()]
        public string WineryAddress { get; set; }
        [Required()]
        public string WineryCity { get; set; }
        [Required()]
        public string WineryStateAbbr { get; set; }
        [Required()]
        public int WineryZip { get; set; }
        [Required()]
        public string WineryPhoneNumber { get; set; }
        [Required()]
        public string Description { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }
        public Winery()
        {

        }
    }
}
