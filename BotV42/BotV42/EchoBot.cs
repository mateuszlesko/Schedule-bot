// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using AdaptiveCards;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace BotV42
{
    /// <summary>
    /// Represents a bot that processes incoming activities.
    /// For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
    /// This is a Transient lifetime service.  Transient lifetime services are created
    /// each time they're requested. For each Activity received, a new instance of this
    /// class is created. Objects that are expensive to construct, or have a lifetime
    /// beyond the single turn, should be carefully managed.
    /// For example, the <see cref="MemoryStorage"/> object and associated
    /// <see cref="IStatePropertyAccessor{T}"/> object are created with a singleton lifetime.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
    public class EchoBot : IBot
    {
        private readonly EchoBotAccessors _accessors;
        private readonly ILogger _logger;
        private DialogSet _dialogSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="EchoWithCounterBot"/> class.
        /// </summary>
        /// <param name="accessors">A class containing <see cref="IStatePropertyAccessor{T}"/> used to manage state.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory"/> that is hooked to the Azure App Service provider.</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1#windows-eventlog-provider"/>
        public EchoBot(EchoBotAccessors accessors )
        {
           

           
           
           this._accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));
            this._dialogSet = new DialogSet(accessors.ConversationDialogState); //wyjatek here
            this._dialogSet.Add(new WaterfallDialog("cardSelector", new WaterfallStep[] { ChoiceCardStepAsync, ShowCardStepAsync }));
            this._dialogSet.Add(new ChoicePrompt("cardPrompt"));
        }

        /// <summary>
        /// Every conversation turn for our Echo Bot will call this method.
        /// There are no dialogs used, since it's "single turn" processing, meaning a single
        /// request and response.
        /// </summary>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn. </param>
        /// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        /// <seealso cref="BotStateSet"/>
        /// <seealso cref="ConversationState"/>
        /// <seealso cref="IMiddleware"/>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            Schedule schedule = new Schedule();

            if (turnContext == null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }


            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                var dialogContext = await this._dialogSet.CreateContextAsync(turnContext, cancellationToken);
                var results = await dialogContext.ContinueDialogAsync(cancellationToken);

                if (results.Status == DialogTurnStatus.Empty)
                {
                    await dialogContext.BeginDialogAsync("cardSelector", cancellationToken: cancellationToken);

                }
            }
            else if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
            {
                
                    await SendWelcomeMessageAsync(turnContext, cancellationToken);
                
            }
            else
            {
                await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected", cancellationToken: cancellationToken);
            }
                // Get the conversation state from the turn context.


                // Bump the turn count for this conversation.


                // Set the property using the accessor.


                // Save the new turn count into the conversation state.
                await _accessors.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
                //var responseMessage = $"You sent '{turnContext.Activity.Text}'\n";


            
        }
        private static async Task SendWelcomeMessageAsync(ITurnContext turn, CancellationToken cancellationToken)
        {
            foreach (var member in turn.Activity.MembersAdded)
            {
                if (member.Id != turn.Activity.Recipient.Id)
                {
                    var reply = turn.Activity.CreateReply();
                    reply.Text = $"Welcome to Schedule Bot {member.Name}.";
                    await turn.SendActivityAsync(reply, cancellationToken);
                }
            }
        }
        private static async Task<DialogTurnResult> ChoiceCardStepAsync(WaterfallStepContext step, CancellationToken cancellationToken)
        {
            return await step.PromptAsync("cardPrompt", GenerateOptions(step.Context.Activity), cancellationToken);
        }
        private static PromptOptions GenerateOptions(Activity activity)
        {
            var options = new PromptOptions()
            {
                Prompt = activity.CreateReply("What to do? Click or type the name of card"),
                Choices = new List<Choice>()
            };

            options.Choices.Add(new Choice() { Value = "Daily Plan" });
            options.Choices.Add(new Choice() { Value = "Weekly Plan" });
            options.Choices.Add(new Choice() { Value = "Current lesson" });
            options.Choices.Add(new Choice() { Value = "Start hour" });
            options.Choices.Add(new Choice() { Value = "End hour " });
            options.Choices.Add(new Choice() { Value = "All subjects" });

            return options;
        }

        public static async Task<DialogTurnResult> ShowCardStepAsync(WaterfallStepContext step, CancellationToken cancellationToken)
        {
            string[] texts = new string[] { "daily", "weekly", "current", "start", "end", "all" };
            var text = step.Context.Activity.Text.ToLowerInvariant().Split(' ')[0];

            var reply = step.Context.Activity.CreateReply();

            reply.Attachments = new List<Attachment>();

            if (text.StartsWith("daily"))
            {
                reply.Attachments.Add(GetDailyPlanCard().ToAttachment());
            }
            if (text.StartsWith("weekly"))
            {
                reply.Attachments.Add(GetWeeklyPlanCard().ToAttachment());
            }
            if (text.StartsWith("current")){
                reply.Attachments.Add(GetCurrentLessonCard().ToAttachment());
            }
            if (text.StartsWith("start"))
            {
                reply.Attachments.Add(GetStartHourCard().ToAttachment());
            }
            if (text.StartsWith("end"))
            {
                reply.Attachments.Add(GetEndHourCard().ToAttachment());
            }
            if (text.StartsWith("all"))
            {
                reply.Attachments.Add(AllSubjectsCard().ToAttachment());
            }
            
            

            await step.Context.SendActivityAsync(reply, cancellationToken);

            await step.Context.SendActivityAsync("Type antything to continue.", cancellationToken: cancellationToken);

            return await step.EndDialogAsync(cancellationToken: cancellationToken);
        }

        private static HeroCard GetDailyPlanCard()
        {
            Schedule s = new Schedule();
            var heroCard = new HeroCard
            {
                Title = $"Today is {DateTime.Now.DayOfWeek.ToString()}",
                Subtitle = "Today's plan:",
                Text = s.getDailyPlan()
                /*"Build and connect intelligent bots to interact with your users naturally wherever they are," +
                       " from text/sms to Skype, Slack, Office 365 mail and other popular services."*/
                
            };

            return heroCard;
        }
        private static HeroCard GetWeeklyPlanCard()
        {
            Schedule s = new Schedule();
            var heroCard = new HeroCard
            {
                Title = "Weekly schedule",
                Text = s.getWeeklyPlan()
            };
            return heroCard;
        }

        private static HeroCard GetCurrentLessonCard()
        {
            Schedule s = new Schedule();
            var heroCard = new HeroCard {
                Title = "Current subject",
                Subtitle = s.getCurrentLesson()
            };
            return heroCard;
        }

        private static HeroCard AllSubjectsCard()
        {
            Schedule s = new Schedule();
            var heroCard = new HeroCard
            {
                Title = "All of school subjects",
                Text = s.getAllSubjects()
            };
            return heroCard;
        }

        private static HeroCard GetStartHourCard()
        {
            Schedule s = new Schedule();
            var heroCard = new HeroCard
            {
                Title = $"Today is {DateTime.Now.DayOfWeek.ToString()}",
                Subtitle = $"School starts at {s.GetWhenEndSchool()} am"
            };
            return heroCard;
        }

        private static HeroCard GetEndHourCard()
        {
            Schedule s = new Schedule();
            var heroCard = new HeroCard
            {
                Title = $"Today is {DateTime.Now.DayOfWeek.ToString()}",
                Subtitle = $"School ends at {s.GetWhenEndSchool()} pm"
            };
            return heroCard;
        }
        private static HeroCard NothingCard()
        {
            var heroCard = new HeroCard
            {
                Title= $"No action",
                Subtitle="Type something to continue"
            };
            return heroCard;
        }
}
}
