using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace SimpleScheduleBot.Forms
{
    [Serializable]
    public enum ScheduleOptions
    {
        GetSchedules=1, AddSchedule=2, UpdateSchedule=3, DeleteSchedule=4
    };
    [Serializable]
    public class WelcomeForm
    {
        [Prompt("Hi, select your {&} and let's get going. {||}")]
        public ScheduleOptions option;
        public static IForm<WelcomeForm> BuildForm()
        {
            return new FormBuilder<WelcomeForm>()
                .Message("Welcome to the Schedule Bot")
                .OnCompletion(async (context, form) => { await context.PostAsync("done"); })
                .Build();
        }
    }
}