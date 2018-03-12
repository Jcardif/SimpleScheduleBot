using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using SimpleScheduleBot.Forms;

namespace SimpleScheduleBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        internal static IDialog<WelcomeForm> MakeRootDialog()
        {
            return Chain.From(() => FormDialog.FromForm(WelcomeForm.BuildForm)).Do(async (context, option) =>
            {
                try
                {
                    var completed = await option;
                    await context.PostAsync("thanks");
                }
                catch (FormCanceledException<WelcomeForm> e)
                {
                    string reply;
                    reply = e.InnerException == null
                        ? $"You quit on {e.Last} -- maybe you can finish next time!"
                        : "Sorry, I've had a short circuit. Please try again.";
                    await context.PostAsync(reply);
                }
            });
        }
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        [ResponseType(typeof(void))]
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity , MakeRootDialog );
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}