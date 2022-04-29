using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using WineryPopulator.DAO;
using System.Threading;
using WineryPopulator.Models;

namespace WineryPopulator
{
    public class Populator
    {
        public readonly string[] BaseApiUrl = { "https://maps.googleapis.com/maps/api/place/textsearch/json?&key=AIzaSyAkc0UP5XUcmIuhl8gm_KaooVpMwuDl0R4&query=Winery%in%cleveland&radius=1000",
                                                "https://maps.googleapis.com/maps/api/place/textsearch/json?key=AIzaSyAkc0UP5XUcmIuhl8gm_KaooVpMwuDl0R4&query=Winery%in%Erie&radius=500"};
        public ISqlDao sqlDao = new SqlDao();
        private List<IRestClient> clients;
        public Populator()
        {
            clients = new List<IRestClient>();
            foreach (string url in BaseApiUrl)
            {
                clients.Add(new RestClient(url));
            }
        }

        public List<WineryData> GetWineries(string BaseUrl, string nextPage, IRestClient client)
        {
            List<WineryData> wineries = new List<WineryData>();
            RestRequest request = new RestRequest()
            {
                Resource = BaseUrl + nextPage
            };
            IRestResponse<Response> restResponse = client.Get<Response>(request);
            Thread.Sleep(2000);
            Response response = restResponse.Data;

            if (response.next_page_token != null)
            {
                nextPage = response.next_page_token;
                nextPage = "&pagetoken=" + nextPage;
                wineries.AddRange(GetWineries(BaseUrl, nextPage, client));
            }
            foreach (WineryData winery in response.Results)
            {
                string newRequest = "https://maps.googleapis.com/maps/api/place/details/json?key=AIzaSyAkc0UP5XUcmIuhl8gm_KaooVpMwuDl0R4&place_id=" + winery.place_id;
                RestRequest phoneNumberRequest = new RestRequest()
                {
                    Resource = newRequest
                };
                IRestResponse<SpecificsResponse> newResponse = client.Get<SpecificsResponse>(phoneNumberRequest);
                winery.phone_number = newResponse.Data.result.formatted_phone_number;
                winery.website = newResponse.Data.result.website;
                wineries.Add(winery);
            }
            return wineries;
        }


        public void populate()
        {
            try
            {
                sqlDao.CreateDatabase();
                List<WineryData> data = new List<WineryData>();
                foreach (IRestClient client in clients)
                {
                    data.AddRange(GetWineries(client.BaseUrl.AbsoluteUri, "", client));
                }

                foreach (WineryData winery in data)
                {
                    sqlDao.CreateWinery(winery);
                }
                sqlDao.FinishDatabase();
            }
            catch
            {
                throw;
            }


        }
    }
}
