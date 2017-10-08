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

        private Task NameReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var name = context.UserData.GetValue<string>("name");

            PromptDialog.Choice(
                context,
                this.AfterUserHasBeenAskForCreation,
                new[] { "Ja", "Nein" },
                StringResources.WelcomeMessage3(name),
                StringResources.Unkown);

            return Task.CompletedTask;
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

        private Task NavigateToDataCollection(IDialogContext context, IAwaitable<object> result)
        {
            context.Call(new StruggleBud.Dialogs.DataCollection.RootDataCollectionDialog(), this.NavigateToExamCollection);
            return Task.CompletedTask;
        }

        private  Task NavigateToExamCollection(IDialogContext context, IAwaitable<object> result)
        {
            context.Call(new StruggleBud.Dialogs.Exams.ExamDataCollectorDialog(), this.NavigateToResult);

            return Task.CompletedTask;
        }

        private Task NavigateToResult(IDialogContext context, IAwaitable<object> result)
        {
            context.Call(new ResultDialog(), this.Restart);

            return Task.CompletedTask;
        }

        private Task Restart(IDialogContext context, IAwaitable<object> result)
        {
            context.UserData.Clear();
            this.StartAsync(context);
            return Task.CompletedTask;
        }

    }
}