using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using StruggleBud.Dialogs.Exams;
using StruggleBud.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StruggleBud.Dialogs.DataCollection.Habits
{

    [Serializable]
    [LuisModel("40684170-88c7-498c-bf2e-14ef2524a7e6", "59bc8914b2174f76ab4d70f5764d90f9")]
    public class ExamPowerLuisDialog : LuisDialog<object>
    {

        [LuisIntent("Lernstunden")]
        private Task ExamPowerInput(IDialogContext context, IAwaitable<object> result, Microsoft.Bot.Builder.Luis.Models.LuisResult luisResult)
        {
            var date = luisResult.Entities[0].Entity;

            var list = new List<string>();

            try
            {
                list = context.UserData.GetValue<List<string>>(UserData.ExamPowers);
            }
            catch (Exception)
            {

            }

            list.Add(date);
            context.UserData.SetValue<List<string>>(UserData.ExamPowers, list);

            context.Done(true);
            return Task.CompletedTask;
        }

        [LuisIntent("")]
        private async Task AbortAsync(IDialogContext context, IAwaitable<object> result, Microsoft.Bot.Builder.Luis.Models.LuisResult luisResult)
        {
            PromptDialog.Choice(
              context,
              this.FallbackSelected,
              new[] { SelectorConstants.PowerFallBackSelectio1, SelectorConstants.PowerFallBackSelectio2 },
              StringResources.FallbackMessage,
              StringResources.Unkown);
        }

        private async Task FallbackSelected(IDialogContext context, IAwaitable<object> result)
        {
            var selector = await result;

            switch (selector)
            {
                case SelectorConstants.PowerFallBackSelectio1:
                    context.Call(new ExamPowerFallbackDialog(), this.Complete);
                    break;
                case SelectorConstants.PowerFallBackSelectio2:
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