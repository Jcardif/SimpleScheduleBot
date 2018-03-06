using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using SimpleScheduleRestClient;
using Simple_Schedule_Database.Models;

namespace SimpleScheduleBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            string command = activity.Text.ToLower();
            List<Schedule> list=new List<Schedule>();
            if (command.Contains("/"))
            {
                string rs=await new ScheduleRestClient().GetAsync(command);
                list = JsonConvert.DeserializeObject<List<Schedule>>(rs);
            }

            string ac = "";
            string lo = "";

            for (int i = 0; i <list.Count; i++)
            {
                ac=String.Concat(ac, $", {list[i].Activity}");
                lo= String.Concat(lo, $", {list[i].Locality}");
            }
            await context.PostAsync($"You have got {list.Count} activities on {Convert.ToDateTime(command):D}, the activities include {ac} and the respective localities are {lo}");
            context.Wait(MessageReceivedAsync);
        }
    }
}