using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestClient
{
    public class ClientLogic
    {
        private const string baseAddress = "http://localhost:52834/";
        private const string contentTypeValue = "application/json";
        private  HttpResponseMessage response;
        public async Task<List<Schedule>> GetSchedules(string date)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentTypeValue));

                response = await client.GetAsync("api/schedule");
                if (response.IsSuccessStatusCode)
                {
                    var scheduleList = await response.Content.ReadAsAsync<List<Schedule>>();
                    return scheduleList;
                }
                else
                {
                    return null;
                }
            }
          


        }
    }
}
