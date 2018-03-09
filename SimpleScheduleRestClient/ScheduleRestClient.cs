using Simple_Schedule_Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace SimpleScheduleRestClient
{
    public class ScheduleRestClient
    {

        public async Task<string> GetAsync(string date)
        {
            var date1=Convert.ToString(Convert.ToDateTime(date).ToString("d"));
            var date2 = date1.Replace("/", "%2F");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52834");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/Json"));
                HttpResponseMessage response;
                response = await client.GetAsync($"api/schedule?date={date2}");
                string result=null;
                if (response.IsSuccessStatusCode)
                {
                     result = await response.Content.ReadAsStringAsync();
                }

                return result;
            }
        }
    }
}
