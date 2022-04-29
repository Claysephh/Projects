using System;
using System.Collections.Generic;
using System.Text;

namespace WineryPopulator
{
    public class Response
    {
        public string next_page_token { get; set; }
        public List<WineryData> Results { get; set; }
        public Response()
        {

        }
    }
}
