using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using StruggleBud.Resources;

namespace StruggleBud.Dialogs
{
    [Serializable]
    public class ResultDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var examSize = context.UserData.GetValue<List<string>>(UserData.ExamDates).Count;
            for (int i = 0; i <= examSize; i++)
            {
                await Magic(context, i);
            }
            await context.PostAsync(StringResources.Result);

            context.Done(true);
        }


        private Task Magic(IDialogContext context, int examNumber)
        {
            var startDate = DateTime.Today.AddDays(1);
            var rawEndDate = context.UserData.GetValue<List<string>>(UserData.ExamDates)[examNumber];
            var rawSubject = context.UserData.GetValue<List<string>>(UserData.ExamSubjects)[examNumber];
            var rawPower = context.UserData.GetValue<List<string>>(UserData.ExamPowers)[examNumber];
            var rawDuration = context.UserData.GetValue<List<string>>(UserData.ExamDurations)[examNumber];
            var powerInMiunutes = Int32.Parse(rawPower) * 60;
            var durationInMinutesPerDay = (Int32.Parse(rawDuration) * 60) / 5;


            var EndDate = DateTime.Parse(rawEndDate);

            try {
                var planer = new StruggleApplication.domain.Planner(context.UserData.GetValue<StruggleApplication.framework.GoogleCalendarInstance>(UserData.UserCalenderToken));
                planer.planExamPreparation(startDate, EndDate, rawSubject, powerInMiunutes, durationInMinutesPerDay);
            } catch(Exception)
            {
                //ignored
            }

            return Task.CompletedTask;
        }
    }

}