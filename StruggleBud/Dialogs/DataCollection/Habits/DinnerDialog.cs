using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StruggleBud.Dialogs.Habits
{
    using System.Threading.Tasks;

    using Microsoft.Bot.Builder.Dialogs;

    using StruggleBud.Dialogs.DataCollection.Habits;
    using StruggleBud.Resources;

    [Serializable]
    public class DinnerDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            PromptDialog.Choice(
                context,
                this.DinnerTimeReceivedAsync,
                new[] {SelectorConstants.DinnerSelesctor1, SelectorConstants.DinnerSelesctor2, SelectorConstants.DinnerSelesctor3, SelectorConstants.DinnerSelesctor4, SelectorConstants.DinnerSelesctor5},
                StringResources.DinnerWelcomeMessage,
                StringResources.Unkown);

            return Task.CompletedTask;
        }

        private async Task DinnerTimeReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {

            var selection = await result;

            switch (selection)
            {
                case SelectorConstants.DinnerSelesctor1:
                    context.UserData.SetValue(UserData.DinnerKey, selection);
                    break;
                case SelectorConstants.DinnerSelesctor2:
                    context.UserData.SetValue(UserData.DinnerKey, selection);
                    break;
                case SelectorConstants.DinnerSelesctor3:
                    context.UserData.SetValue(UserData.DinnerKey, selection);
                    break;
                case SelectorConstants.DinnerSelesctor4:
                    context.UserData.SetValue(UserData.DinnerKey, string.Empty);
                    break;
                case SelectorConstants.DinnerSelesctor5:
                    await this.CallDinnerLuisDialogAsync(context);
                    return;
            }

            await AskUserForConfirmation(context, context.UserData.GetValue<string>(UserData.DinnerKey));
        }

        private async Task SmartDinnerFinishedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await AskUserForConfirmation(context, context.UserData.GetValue<string>(UserData.DinnerKey));
        }

        private Task CallDinnerLuisDialogAsync(IDialogContext context)
        {
            context.Call(new DinnerLuisDialog(), this.SmartDinnerFinishedAsync);
            return Task.CompletedTask;

        }

        private  Task AskUserForConfirmation(IDialogContext context, string time)
        {
            var question = StringResources.DinnerConfirmationMessage(time);
            if (string.IsNullOrEmpty(time))
            {
                question = StringResources.DinnerNoMessage;
            }

            PromptDialog.Choice(
                context,
                this.SelectionReceivedAsync,
                new[] { "Ja", "Nein" },
                question,
                StringResources.Unkown);

            return Task.CompletedTask;
        }

        private async Task SelectionReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var selection = await result;
            switch (selection)
            {
                case "Ja":
                    await context.PostAsync(StringResources.DinnerDoneMessage);
                    context.Done(true);
                    break;
                case "Nein":
                    await this.StartAsync(context);
                    break;
            }
        }
    }

}