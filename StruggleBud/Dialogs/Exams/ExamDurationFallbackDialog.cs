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
    public class ExamDurationFallbackDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(this.DurationReceivedAsync);
            return Task.CompletedTask;
        }
        
        private async Task DurationReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            var duration = activity.Text;

            UserDataUtility.AddDurationToUserData(context, duration);

            context.Done(true);
        }
    }

}