using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace SimpleScheduleBot.Dialogs
{
    [Serializable]
    public class GetScheduleDialog :IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {

            await context.PostAsync("Getting all schedules");
            context.Wait(MessageRecievedAsync);
        }

        private async Task MessageRecievedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            await context.PostAsync("Getting all schedules");
            context.Wait(MessageRecievedAsync);
        }
    }
}