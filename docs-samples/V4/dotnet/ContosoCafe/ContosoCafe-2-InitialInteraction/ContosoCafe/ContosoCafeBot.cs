﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

namespace ContosoCafe
{
    public class ContosoCafeBot : IBot
    {
        public async Task OnTurn(ITurnContext context)
        {
            // Choose what to do based on the incoming activity type.
            switch (context.Activity.Type)
            {
                case ActivityTypes.ConversationUpdate:

                    // If a user is being added to the conversation, send the greeting message.
                    var update = context.Activity.AsConversationUpdateActivity();
                    if (update.MembersAdded.Any(member => member.Id != update.Recipient.Id))
                    {
                        await context.SendActivities(
                            new IActivity[]
                            {
                                MessageFactory.Text("Hello, I'm the ContosoCafe bot."),
                                MessageFactory.Text("How can I help you?")
                            });
                    }
                    break;

                case ActivityTypes.Message:

                    // Handle any message the user sends to the bot.
                    await ProcessMessage(context);
                    break;
            }
        }

        /// <summary>
        /// Handle any message the user sends to the bot.
        /// </summary>
        /// <param name="context">The bot's current turn context.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        private static async Task ProcessMessage(ITurnContext context)
        {
            // Capture any input text, and stub in responses for some of the input we want to handle.
            var text = context.Activity.AsMessageActivity()?.Text?.Trim().ToLowerInvariant();
            switch (text)
            {
                case "help":
                    // Provide some guidance to the user.
                    await context.SendActivity("Type `book a table` to make a reservation.");
                    break;

                case "who are you":
                case "who are you?":
                    // Answer the "who are you" question.
                    await context.SendActivity("Hi, I'm the Contoso Cafe bot.");
                    break;

                case "book table":
                case "book a table":
                    // Stub in the "book a table" conversation logic.
                    var typing = Activity.CreateTypingActivity();
                    var delay = new Activity { Type = "delay", Value = 5000 };
                    await context.SendActivities(
                        new IActivity[]
                        {
                            typing, delay,
                            MessageFactory.Text("Your table is booked. Reference number: #K89HG38SZ")
                        });
                    break;

                default:
                    // Provide a default response for anything we didn't understand.
                    await context.SendActivity("I'm sorry; I do not understand.");
                    break;
            }
        }
    }
}
