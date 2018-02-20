using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using RestClient;


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
            if (command.Contains("get all"))
            {
                ClientLogic cl = new ClientLogic();
                List<Schedule> schedules = await cl.GetSchedules();
                await context.PostAsync($"You have {schedules.Count} schedules today.");
            }

            context.Wait(MessageReceivedAsync);
        }
    }
}