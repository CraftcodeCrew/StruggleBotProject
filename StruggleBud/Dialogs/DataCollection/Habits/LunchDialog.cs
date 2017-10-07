using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StruggleBud.Dialogs.Habits
{
    using System.Threading.Tasks;

    using Microsoft.Bot.Builder.Dialogs;

    using StruggleBud.Resources;

    [Serializable]
    public class LunchDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            PromptDialog.Choice(
                context,
                this.LunchTimeReceivedAsync,
                new[] {SelectorConstants.LunchSelesctor1, SelectorConstants.LunchSelesctor2, SelectorConstants.LunchSelesctor3, SelectorConstants.LunchSelesctor4,
                          SelectorConstants.LunchSelesctor5},
                StringResources.LaunchWelcomeMessage,
                StringResources.Unkown);
        }

        private async Task LunchTimeReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {

            var selection = await result;

            switch (selection)
            {
                case SelectorConstants.LunchSelesctor1:
                    context.UserData.SetValue(UserData.LunchKey, selection);
                    break;
                case SelectorConstants.LunchSelesctor2:
                    context.UserData.SetValue(UserData.LunchKey, selection);
                    break;
                case SelectorConstants.LunchSelesctor3:
                    context.UserData.SetValue(UserData.LunchKey, selection);
                    break;
                case SelectorConstants.LunchSelesctor4:
                    context.UserData.SetValue(UserData.LunchKey, string.Empty);
                    break;
                case SelectorConstants.LunchSelesctor5:
                    context.Done(true);
                    break;
            }

            await AskUserForConfirmation(context, context.UserData.GetValue<string>(UserData.LunchKey));
        }

        private async Task AskUserForConfirmation(IDialogContext context, string time)
        {
            var question = StringResources.LunchConfirmationMessage(time);
            if (string.IsNullOrEmpty(time))
            {
                question = StringResources.NoLunchMessage;
            }

            PromptDialog.Choice(
                context,
                this.SelectionReceivedAsync,
                new[] {"Ja", "Nein"},
                question,
                StringResources.Unkown);
        }

        private async Task SelectionReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var selection = await result;
            switch (selection)
            {
                case "Ja":
                    await context.PostAsync(StringResources.LunchDoneMessage);
                    context.Done(true);
                    break;
                case "Nein":
                    await this.StartAsync(context);
                    break;   
            }
        }
    }
}