using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace StruggleBud.Dialogs
{
    using System.Collections.Generic;

    using StruggleBud.Resources;
    using Microsoft.Bot.Builder.Luis;

    using StruggleBud.Dialogs.Intelligence;

    [Serializable]
    public class RootDialog : IDialog
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(this.WelcomeMessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task WelcomeMessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync(StringResources.WelcomeMessage1);
            await Task.Delay(1000);
            await context.PostAsync(StringResources.WelcomeMessage2);

            context.Call(new NameDialog(), this.NameReceivedAsync);
        }

        private async Task NameReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
           context.Call(new WelcomeLuisDialog(), this.WelcomeMessageReceivedAsync);
        }

    }
}