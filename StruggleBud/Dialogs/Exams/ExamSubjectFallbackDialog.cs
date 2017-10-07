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
    public class ExamSubjectFallbackDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(this.SubjectReceivedAsync);
           return Task.CompletedTask;

        }


        private async Task SubjectReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            var subject = activity.Text;

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
    }

}