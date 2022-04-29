using System;
using System.Collections.Generic;
using System.Text;

namespace WineryPopulator.Models
{
    public class Address
    {
        public string winery_address { get; set; }
        public string winery_city { get; set; }
        public string winery_state_abbr { get; set; }
        public int winery_zip { get; set; }
        public string winery_country { get; set; }
    }
}
