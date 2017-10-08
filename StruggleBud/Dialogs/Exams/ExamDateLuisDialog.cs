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
    public class ExamDateLuisDialog : LuisDialog<object>
    {

        [LuisIntent("Datetime")]
        private Task ExamDateInput(IDialogContext context, IAwaitable<object> result, Microsoft.Bot.Builder.Luis.Models.LuisResult luisResult)
        {
          
            var date = luisResult.Entities[luisResult.Entities.Count - 1].Entity;

            var list = new List<string>();

            try { 
             list = context.UserData.GetValue<List<string>>(UserData.ExamDates);
            } catch(Exception)
            {
                //ignored
            }

            list.Add(date);
            context.UserData.SetValue<List<string>>(UserData.ExamDates, list);

            context.Done(true);
            return Task.CompletedTask;
        }

        [LuisIntent("")]
        private Task AbortAsync(IDialogContext context, IAwaitable<object> result, Microsoft.Bot.Builder.Luis.Models.LuisResult luisResult)
        {
             PromptDialog.Choice(
               context,
               this.FallbackSelected,
               new[] { SelectorConstants.DateFallBackSelectio1, SelectorConstants.DateFallBackSelectio2},
               StringResources.FallbackMessage,
               StringResources.Unkown);

            return Task.CompletedTask;
        }

        private async Task FallbackSelected(IDialogContext context, IAwaitable<object> result)
        {
            var selector = await result;

            switch (selector)
            {
                case SelectorConstants.DateFallBackSelectio1:
                    context.Call(new ExamDateFallbackDialog(), this.Complete);
                    break;
                case SelectorConstants.DateFallBackSelectio2:
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