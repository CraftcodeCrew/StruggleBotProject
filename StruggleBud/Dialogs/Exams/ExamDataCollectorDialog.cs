using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using StruggleBud.Resources;
using StruggleBud.Dialogs.InitDialogs;

namespace StruggleBud.Dialogs.Exams
{
    [Serializable]
    public class ExamDataCollectorDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await ConfirmExamEnteringLoop(context);
        }

        private  Task ConfirmExamEnteringLoop(IDialogContext context)
        {
            PromptDialog.Choice(
                context,
                this.SelectionReceivedAsync,
                new[] { "Ja", "Nein" },
                StringResources.ExamWelcomeMessage,
                StringResources.Unkown);

            return Task.CompletedTask;
        }

        private async Task SelectionReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var selection = await result;
            switch (selection)
            {
                case "Ja":
                    await EnterLoopAsync(context, result);
                    break;
                case "Nein":
                    context.Call(new CreationAbortDialog(), this.EnterLoopAsync);
                    break;
            }
        }

        private async Task EnterLoopAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync(StringResources.ExamLoopBeginMessage);
            context.Call(new LoopingExamDialog(), this.LoopCompletedAsync);
        }

        private Task LoopCompletedAsync(IDialogContext context, IAwaitable<object> result)
        {
            PromptDialog.Choice(
             context,
             this.ReenterSelectionReceivedAsync,
             new[] { "Ja", "Nein" },
             StringResources.ExamReenterLoop,
             StringResources.Unkown);

            return Task.CompletedTask;
        }

        private async Task ReenterSelectionReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var selection = await result;
            switch (selection)
            {
                case "Ja":
                    await EnterLoopAsync(context, result);
                    return;
                case "Nein":
                    context.Done(true);
                    return;
            }
        }
    }
}