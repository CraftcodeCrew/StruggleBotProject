using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using StruggleBud.Resources;
using StruggleBud.Dialogs.DataCollection.Habits;

namespace StruggleBud.Dialogs.Habits
{
    [Serializable]
    public class BreakfastDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await this.BreakfastTimeSelectorAsync(context, null);
        }

        private async Task BreakfastTimeSelectorAsync(IDialogContext context, IAwaitable<object> result)
        {
            PromptDialog.Choice(
                context,
                this.BreakfastTimeReceivedAsync,
                new[] {SelectorConstants.BreakfastSelector1, SelectorConstants.BreakfastSelector2, SelectorConstants.BreakfastSelector3, SelectorConstants.BreakfastSelector4,
                SelectorConstants.BreakfastSelector5, SelectorConstants.BreakfastSelector6},
                StringResources.BreakfastTimeBlockerMessage,
                StringResources.Unkown);
        }

        private async Task BreakfastTimeReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            
            var selection = await result;

            switch (selection)
            {
                case SelectorConstants.BreakfastSelector1:
                    context.UserData.SetValue(UserData.BreakFastKey, selection);
                    break;
                case SelectorConstants.BreakfastSelector2:
                    context.UserData.SetValue(UserData.BreakFastKey, selection);
                    break;
                case SelectorConstants.BreakfastSelector3:
                    context.UserData.SetValue(UserData.BreakFastKey, selection);
                    break;
                case SelectorConstants.BreakfastSelector4:
                    context.UserData.SetValue(UserData.BreakFastKey, selection);
                    break;
                case SelectorConstants.BreakfastSelector5:
                    //TODO user output
                    context.UserData.SetValue(UserData.BreakFastKey, selection);
                    break;
                case SelectorConstants.BreakfastSelector6:
                    await CallBreakFastLuisDialogAsync(context, result);
                    return;                 
              
            }
            await AskUserForConfirmation(context, context.UserData.GetValue<string>(UserData.BreakFastKey));
        }

        private Task CallBreakFastLuisDialogAsync(IDialogContext context, IAwaitable<object> result)
        {
            context.Call(new BreakfastLuisDialog(), this.SmartBreakfastFinishedAsync);
            return Task.CompletedTask;

        }

        private async Task SmartBreakfastFinishedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await AskUserForConfirmation(context, context.UserData.GetValue<string>(UserData.BreakFastKey));
        }


        private async Task AskUserForConfirmation(IDialogContext context, string time)
        {
            PromptDialog.Choice(
                context,
                this.SelectionReceivedAsync,
                new[] { "Ja", "Nein" },
                StringResources.BreakfastTimeSetMessage(time),
                StringResources.Unkown);
        }

        private async Task SelectionReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {

            var selection = await result;
            switch (selection)
            {
                case "Ja":
                    await context.PostAsync(StringResources.BreakfastConfirmationMessage);
                    context.Done(true);
                    break;
                case "Nein":
                    await this.StartAsync(context);
                    break;
            }
        }
    }
}