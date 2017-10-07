using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StruggleBud.Dialogs.Intelligence
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Luis;
    using Microsoft.Bot.Connector;

    using StruggleBud.Resources;

    [Serializable]
    [LuisModel("40684170-88c7-498c-bf2e-14ef2524a7e6", "59bc8914b2174f76ab4d70f5764d90f9")]
    public class NameDialog : LuisDialog<object>
    {

        [LuisIntent("NameCreation")]
        private async Task NameReceivedAsync(IDialogContext context, IAwaitable<object> result, Microsoft.Bot.Builder.Luis.Models.LuisResult luisResult)
        {
            var name = luisResult.Entities[0].Entity;

            if (string.IsNullOrEmpty(name))
            {
                await context.PostAsync(StringResources.Unkown);
                return;
            }

            // return our reply to the user
            context.UserData.SetValue("name", name);

           context.Done(true);
        }

        [LuisIntent("")]
        private async Task Unkown(IDialogContext context, IAwaitable<object> result, Microsoft.Bot.Builder.Luis.Models.LuisResult luisResult)
        {
            await context.PostAsync(StringResources.Unkown);
            context.Done(true);
        }
    }
}