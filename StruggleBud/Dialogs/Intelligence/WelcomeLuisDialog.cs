using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace StruggleBud.Dialogs
{
    using System.Collections.Generic;

    using StruggleBud.Resources;
    using Microsoft.Bot.Builder.Luis;

    [Serializable]
    [LuisModel("40684170-88c7-498c-bf2e-14ef2524a7e6", "59bc8914b2174f76ab4d70f5764d90f9")]
    public class WelcomeLuisDialog : LuisDialog<object>
    {
  
        [LuisIntent("Confirm")]
        private async Task ConfirmationAsync(IDialogContext context, IAwaitable<object> result, Microsoft.Bot.Builder.Luis.Models.LuisResult luisResult)
        {
            await context.PostAsync("Penis");
            context.Done(true);
        }

        [LuisIntent("Abort")]
        private async Task AbortAsync(IDialogContext context, IAwaitable<object> result, Microsoft.Bot.Builder.Luis.Models.LuisResult luisResult)
        {
            await context.PostAsync("Dödel");
            context.Done(true);
        }

    }
}