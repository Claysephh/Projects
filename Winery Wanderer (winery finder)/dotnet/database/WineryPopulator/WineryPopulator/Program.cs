using System;

namespace WineryPopulator
{
    class Program
    {
        private const string apiUrl = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=Winery in cleveland&radius=500&key=AIzaSyAkc0UP5XUcmIuhl8gm_KaooVpMwuDl0R4";
        static void Main()
        {
            Populator app = new Populator();
            app.populate();
        }
    }
}
