using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StruggleBud.Dialogs.DataCollection
{
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    using StruggleBud.Resources;

    [Serializable]
    public class FinishDataCollectionDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var username = context.UserData.GetValue<string>(UserData.NameKey);

            var breakfastTime = context.UserData.GetValue<string>(UserData.BreakFastKey);
            var lunchTime = context.UserData.GetValue<string>(UserData.LunchKey);
            var dinnerTime = context.UserData.GetValue<string>(UserData.DinnerKey);
            var sleepTime = context.UserData.GetValue<string>(UserData.SleepKey);

            var breakfastIsSet = !string.IsNullOrEmpty(breakfastTime);
            var lunchTimeIsSet = !string.IsNullOrEmpty(lunchTime);
            var dinnerTimeIsSet = !string.IsNullOrEmpty(dinnerTime);
            var sleepTimeIsSet = !string.IsNullOrEmpty(sleepTime);


            var builder = new StringBuilder();
            builder.AppendLine("*Das hab ich mir gemerkt! :)*");
            builder.AppendLine(string.Empty);

            if (breakfastIsSet)
            {
                builder.AppendLine($"Frühstück: _{breakfastTime}_");
            }
            else
            {
                builder.AppendLine("Kein Fürhstück für dich!");
            }

            builder.AppendLine(string.Empty);

            if (lunchTimeIsSet)
            {
                builder.AppendLine($"Mittagessen: _{lunchTime}_");
            }
            else
            {
                builder.AppendLine("Kein Mittag für dich!");
            }

            builder.AppendLine(string.Empty);

            if (dinnerTimeIsSet)
            {
                builder.AppendLine($"Dein Abendbrot: _{dinnerTime}_");
            }
            else
            {
                builder.AppendLine("Kein Abendessen für dich!");
            }

            builder.AppendLine(string.Empty);

            builder.AppendLine($"Zeit für die Falle ist um: _{sleepTime}_");

            var card = new HeroCard()
                                      {
                                          Text = builder.ToString()
                                      };

            IMessageActivity message = context.MakeMessage();
            message.Text = StringResources.FinishWelcomeMessage(username);
            message.Locale = "de-De";
            

            var plAttachment = card.ToAttachment();
            var attachments = new List<Attachment>();
            attachments.Add(plAttachment);
            message.Attachments = attachments;

           await context.PostAsync(message);
           context.Done(true);
        }
    }
}