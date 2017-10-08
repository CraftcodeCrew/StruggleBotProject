using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using StruggleBud.Dialogs.Exams;
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
            PromptDialog.Choice(
                context,
                this.FallbackSelected,
                new[] { SelectorConstants.DurationFallBackSelectio1, SelectorConstants.DurationFallBackSelectio2 },
                StringResources.ExamLoop6,
                StringResources.Unkown);
        }

        private async Task FallbackSelected(IDialogContext context, IAwaitable<object> result)
        {
            var selector = await result;

            switch (selector)
            {
                case SelectorConstants.DurationFallBackSelectio1:
                    context.Call(new ExamDurationFallbackDialog(), this.Complete);
                    break;
                case SelectorConstants.DurationFallBackSelectio2:
                    await context.PostAsync(StringResources.FallbackSkio);
                    break;
            }
        }

        private Task Complete(IDialogContext context, IAwaitable<object> result)
        {
            context.Done(true);
            return Task.CompletedTask;
        }

    }
}