using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using StruggleBud.Resources;
using StruggleBud.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StruggleBud.Dialogs.DataCollection.Habits
{

    [Serializable]
    [LuisModel("40684170-88c7-498c-bf2e-14ef2524a7e6", "59bc8914b2174f76ab4d70f5764d90f9")]
    public class ExamDurationLuisDialog : LuisDialog<object>
    {

        [LuisIntent("Lernstunden")]
        private Task ExamDurationInput(IDialogContext context, IAwaitable<object> result, Microsoft.Bot.Builder.Luis.Models.LuisResult luisResult)
        {
            var duration = luisResult.Entities[0].Entity;

            UserDataUtility.AddDurationToUserData(context, duration as string);

            context.Done(true);
            return Task.CompletedTask;
        }

        [LuisIntent("")]
        private async Task AbortAsync(IDialogContext context, IAwaitable<object> result, Microsoft.Bot.Builder.Luis.Models.LuisResult luisResult)
        {
            await context.PostAsync(StringResources.CalenderAccessFailed);
        }

    }
}