using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using StruggleBud.Resources;
using StruggleBud.Dialogs.InitDialogs;
using Microsoft.Bot.Connector;

namespace StruggleBud.Dialogs.Exams
{
    [Serializable]
    public class ExamDateFallbackDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(this.DateReceivedAsync);
            return Task.CompletedTask;
        }


        private async Task DateReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            var date = activity.Text;

            var list = new List<string>();

            try
            {
                list = context.UserData.GetValue<List<string>>(UserData.ExamDates);
            }
            catch (Exception)
            {

            }

            list.Add(date);
            context.UserData.SetValue<List<string>>(UserData.ExamDates, list);

            context.Done(true);
        }
    }

}