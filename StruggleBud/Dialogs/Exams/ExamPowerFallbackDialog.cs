using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using StruggleBud.Resources;
using StruggleBud.Dialogs.InitDialogs;
using StruggleBud.Utility;
using Microsoft.Bot.Connector;

namespace StruggleBud.Dialogs.Exams
{
    [Serializable]
    public class ExamPowerFallbackDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(this.PowerReceivedAsync);
            return Task.CompletedTask;
        }
        
        private async Task PowerReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            var date = activity.Text;

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
        }
    }

}