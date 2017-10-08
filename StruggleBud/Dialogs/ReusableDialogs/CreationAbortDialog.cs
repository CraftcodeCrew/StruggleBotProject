using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StruggleBud.Dialogs.InitDialogs
{
    using System.Threading.Tasks;

    using Microsoft.Bot.Builder.Dialogs;

    using StruggleBud.Resources;

    [Serializable]
    public class CreationAbortDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync(StringResources.CreationAboard1);

            context.Wait(this.WaitForBullshit);
        }

        private  Task WaitForBullshit(IDialogContext context, IAwaitable<object> result)
        {

            PromptDialog.Choice(
                context,
                this.AfterSelection,
                new[] { "Ja", "Nein" },
                StringResources.CreationAboard2,
                StringResources.Unkown);

            return Task.CompletedTask;
        }

        private async Task AfterSelection(IDialogContext context, IAwaitable<object> result)
        {
            var selection = await result;

            switch (selection)
            {
                case "Ja":
                    context.Done(true);
                    break;

                case "Nein":
                    await this.StartAsync(context);
                    break;
            }

        }

    }
}