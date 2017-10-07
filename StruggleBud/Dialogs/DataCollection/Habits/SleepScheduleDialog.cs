using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using StruggleBud.Resources;
using StruggleBud.Dialogs.DataCollection.Habits;
using System.Diagnostics;

namespace StruggleBud.Dialogs.Habits
{
    [Serializable]
    public class SleepScheduleDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            PromptDialog.Choice(
                context,
                this.SleepTimeReceivedAsync,
                new[] { SelectorConstants.SleepSelesctor1, SelectorConstants.SleepSelesctor2, SelectorConstants.SleepSelesctor3, SelectorConstants.SleepSelesctor4, SelectorConstants.SleepSelesctor5 },
                StringResources.SleepWelcomeMessage,
                StringResources.Unkown);

            return Task.CompletedTask;
        }

        private async Task SleepTimeReceivedAsync(IDialogContext context, IAwaitable<string> result)
        {
            var selection = await result;

            switch (selection)
            {
                case SelectorConstants.SleepSelesctor1:
                    context.UserData.SetValue(UserData.SleepKey, selection);
                    break;
                case SelectorConstants.SleepSelesctor2:
                    context.UserData.SetValue(UserData.SleepKey, selection);
                    break;
                case SelectorConstants.SleepSelesctor3:
                    context.UserData.SetValue(UserData.SleepKey, selection);
                    break;
                case SelectorConstants.SleepSelesctor4:
                    context.UserData.SetValue(UserData.SleepKey, string.Empty);
                    break;
                case SelectorConstants.SleepSelesctor5:
                    await this.CallSleepLuisDialogAsync(context);
                    return;
            }

            await this.AskUserForConfirmation(context, context.UserData.GetValue<string>(UserData.SleepKey));
        }


        private async Task SmartSleepFinishedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await AskUserForConfirmation(context, context.UserData.GetValue<string>(UserData.SleepKey));
        }

        private Task CallSleepLuisDialogAsync(IDialogContext context)
        {
            context.Call(new SleepLuisDialog(), this.SmartSleepFinishedAsync);
            return Task.CompletedTask;

        }

        private async Task AskUserForConfirmation(IDialogContext context, string time)
        {
            var question = StringResources.SleepConfirmationMessage(time);
            if (string.IsNullOrEmpty(time))
            {
                await context.PostAsync(StringResources.NoSleepMessage);
                await this.StartAsync(context);
                return;
            }

            PromptDialog.Choice(
                context,
                this.SelectionReceivedAsync,
                new[] { "Ja", "Nein" },
                question,
                StringResources.Unkown);
        }

        private async Task SelectionReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var selection = await result;
            switch (selection)
            {
                case "Ja":
                    await context.PostAsync(StringResources.SleepDoneMessage);
                    context.Done(true);
                    break;
                case "Nein":
                    await this.StartAsync(context);
                    break;
            }
        }

    }
}