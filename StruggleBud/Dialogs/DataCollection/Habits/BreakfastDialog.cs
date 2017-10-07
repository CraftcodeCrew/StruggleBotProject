using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using StruggleBud.Resources;

namespace StruggleBud.Dialogs.Habits
{
    [Serializable]
    public class BreakfastDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(this.TimeBlockerMessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task TimeBlockerMessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync(StringResources.WelcomeMessage1);
            await Task.Delay(1000);
            await context.PostAsync(StringResources.WelcomeMessage2);

            context.Call(new BreakfastTimesLuisDialog(), this.BreakfastTimesReceivedAsync);
        }

        private async Task BreakfastTimesReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            throw new NotImplementedException();
        }
    }
}