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
    public class ExamSubjectLuisDialog : LuisDialog<object>
    {

        [LuisIntent("Klausurfach")]
        private async Task ExamPowerInput(IDialogContext context, IAwaitable<object> result, Microsoft.Bot.Builder.Luis.Models.LuisResult luisResult)
        {
            var subject = string.Empty;
            try {
                subject = luisResult.Entities[0].Entity;
            }
            catch (Exception)
            {

                await SwitchToFallback(context);
                return;
            }
            var list = new List<string>();

            try
            {
                list = context.UserData.GetValue<List<string>>(UserData.ExamSubjects);
            }
            catch (Exception)
            {

            }

            list.Add(subject);
            context.UserData.SetValue<List<string>>(UserData.ExamSubjects, list);

            context.Done(true);
          
        }

        private  Task SwitchToFallback(IDialogContext context)
        {
            PromptDialog.Choice(
              context,
              this.FallbackSelected,
              new[] { SelectorConstants.SubjectFallBackSelectio1, SelectorConstants.SubjectFallBackSelectio2 },
              StringResources.FallbackMessage,
              StringResources.Unkown);

            return Task.CompletedTask;
        }

        private async Task FallbackSelected(IDialogContext context, IAwaitable<object> result)
        {
            var selector = await result;

            switch (selector)
            {
                case SelectorConstants.SubjectFallBackSelectio1:
                    context.Call(new ExamSubjectFallbackDialog(), this.Complete);
                    break;
                case SelectorConstants.SubjectFallBackSelectio2:
                    await context.PostAsync(StringResources.FallbackSkio);
                    break;
            }
        }

        private Task Complete(IDialogContext context, IAwaitable<object> result)
        {
            context.Done(true);
            return Task.CompletedTask;
        }

        [LuisIntent("")]
        private async Task AbortAsync(IDialogContext context, IAwaitable<object> result, Microsoft.Bot.Builder.Luis.Models.LuisResult luisResult)
        {
            await SwitchToFallback(context);
        }

    }
}