using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
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

                subject = "Unbekannte Klausur";
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

        [LuisIntent("")]
        private async Task AbortAsync(IDialogContext context, IAwaitable<object> result, Microsoft.Bot.Builder.Luis.Models.LuisResult luisResult)
        {
            await context.PostAsync(StringResources.CalenderAccessFailed);
        }

    }
}