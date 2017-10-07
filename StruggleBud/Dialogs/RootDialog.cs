using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace StruggleBud.Dialogs
{
    using System.Collections.Generic;

    using StruggleBud.Resources;
    using Microsoft.Bot.Builder.Luis;

    using StruggleBud.Dialogs.InitDialogs;
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
            var name = context.UserData.GetValue<string>("name");

            PromptDialog.Choice(
                context,
                this.AfterUserHasBeenAskForCreation,
                new[] { "Ja", "Nein" },
                StringResources.WelcomeMessage3(name),
                StringResources.Unkown);
        }


        private async Task AfterUserHasBeenAskForCreation(IDialogContext context, IAwaitable<object> result)
        {
                var selection = await result;

                switch (selection)
                {
                    case "Ja":
                    await this.NavigateToDataCollection(context, result);
                        break;

                    case "Nein":
                        context.Call(new CreationAbortDialog(), this.NavigateToDataCollection);
                        break;
                }
        }

        private async Task NavigateToDataCollection(IDialogContext context, IAwaitable<object> result)
        {
            context.Call(new StruggleBud.Dialogs.DataCollection.RootDataCollectionDialog(), this.NavigateToExamCollection);
        }

        private async Task NavigateToExamCollection(IDialogContext context, IAwaitable<object> result)
        {
            context.Call(new StruggleBud.Dialogs.Exams.ExamDataCollectorDialog(), this.WelcomeMessageReceivedAsync);
        }

    }
}