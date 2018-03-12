using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Internals.Fibers;
using SimpleScheduleBot.Dialogs;

namespace SimpleScheduleBot.Forms
{
    [Serializable]
    public  enum ScheduleOptions
    {
        GetSchedules=1, AddSchedule=2, UpdateSchedule=3, DeleteSchedule=4
    };
    [Serializable]
    public class WelcomeForm
    {
        [Prompt("Hi, select your {&} and let's get going. {||}")]
        public  ScheduleOptions Option;
        public static IForm<WelcomeForm> BuildForm()
        {
            return new FormBuilder<WelcomeForm>()
                .Message("Welcome to the Schedule Bot")
                .OnCompletion(async (context, option) =>
                {
                    switch (option.Option)
                    {
                        case ScheduleOptions.GetSchedules:
                            context.Call(new GetScheduleDialog(), ResumeAfterTasks);
                            break;
                        case ScheduleOptions.AddSchedule:
                            await context.PostAsync("Okay here we go-add");
                            break;
                        case ScheduleOptions.UpdateSchedule:
                            await context.PostAsync("Okay here we go-update");
                            break;
                        case ScheduleOptions.DeleteSchedule:
                            await context.PostAsync("Okay here we go");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                })
                .Build();
        
        }

        private static async Task ResumeAfterTasks(IDialogContext context, IAwaitable<object> result)
        {
            await Task.Run(() => context.Call(new GetScheduleDialog(), ResumeAfterTasks));
        }
    }
}