using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace StruggleBud.Dialogs
{
    using System.Collections.Generic;

    using StruggleBud.Resources;

    [Serializable]
    public class RootDialog : IDialog<object>
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

            context.Wait(this.NameReceivedAsync);
        }

        private async Task NameReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            var name = activity?.Text;

            if (string.IsNullOrEmpty(name))
            {
                context.Wait(this.WelcomeMessageReceivedAsync);
                return;
            }

            // return our reply to the user
            await context.PostAsync(StringResources.WelcomeMessage3(name));

            context.Wait(this.WelcomeMessageReceivedAsync);
        }
    }
}