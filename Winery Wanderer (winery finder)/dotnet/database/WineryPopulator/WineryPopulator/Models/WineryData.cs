using System;
using System.Collections.Generic;
using System.Text;
using WineryPopulator.Models;
using RestSharp;

namespace WineryPopulator
{
    public class WineryData
    {
        public string name { get; set; }
        public string formatted_address { get; set; }
        public string business_status { get; set; }
        public bool status { get; set; }
        public List<Photo> photos { get; set; }
        public Address address { get; set; }
        public string place_id { get; set; }
        public string phone_number { get; set; }
        public List<string> types { get; set; }
        public string description { get; set; }
        public string website { get; set; }
        public void autofill()
        {
            string[] addressInfo = this.formatted_address.Split(", ");
            string[] state_zip = addressInfo[2].Split(" ");
            if (addressInfo.Length == 4 && int.TryParse(state_zip[1], out int i))
            {
                this.address = new Address()
                {
                    winery_address = addressInfo[0].Trim(','),
                    winery_city = addressInfo[1].Trim(','),
                    winery_country = addressInfo[3].Trim(','),
                    winery_state_abbr = state_zip[0],
                    winery_zip = int.Parse(state_zip[1]),
                };
            }
            else
            {
                this.address = new Address()
                {
                    winery_address = "IMPROPER ADDRESS",
                    winery_city = "",
                    winery_country = "",
                    winery_state_abbr = "",
                    winery_zip = 0,
                };
                this.business_status = "";
            }
            if(phone_number == null)
            {
                this.phone_number = "";
            }
            if(website == null)
            {
                this.website = "";
            }
            if(this. types != null)
            {
                for(int x = 0; x < types.Count - 1; x++)
                {
                    description += types[x].Replace("_", " ") + ", ";
                }
                description += types[types.Count - 1] + ". " + this.website;
                
            }

            if (name == null)
            {
                name = "";
            }
            if (photos == null)
            {
                photos = new List<Photo>();
                photos.Add(new Photo()
                {
                    photo_reference = ""
                });
            }
            else 
            {
                photos[0].photo_reference = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=700&key=AIzaSyAkc0UP5XUcmIuhl8gm_KaooVpMwuDl0R4&photo_reference=" + photos[0].photo_reference;
            }
            if (business_status == "OPERATIONAL")
            {
                this.status = true;
            }
            else
            {
                this.status = false;
            }
        }


    }
}

