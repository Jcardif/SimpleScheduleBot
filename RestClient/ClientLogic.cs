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
        private HttpClient client;
        private const string baseAddress = "http://localhost:52834/";
        private const string contentTypeValue = "application/json";
        private  HttpResponseMessage response;

        public async Task<List<Schedule>> GetSchedules()
        {
            using (var client1 = new HttpClient())
            {
                client1.BaseAddress = new Uri(baseAddress);
                client1.DefaultRequestHeaders.Accept.Clear();
                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentTypeValue));
                response = await client1.GetAsync("api/schedule");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var scheduleList = JsonConvert.DeserializeObject<List<Schedule>>(result);
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
