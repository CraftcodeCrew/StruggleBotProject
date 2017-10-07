using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using StruggleBud.Resources;
using StruggleBud.Dialogs.InitDialogs;
using StruggleBud.Dialogs.DataCollection.Habits;
using StruggleBud.Utility;

namespace StruggleBud.Dialogs.Exams
{
    [Serializable]
    public class LoopingExamDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync(StringResources.ExamLoop1);
            context.Call(new ExamDateLuisDialog(), this.DateCompletedAsync);
        }

        private async Task DateCompletedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var date = context.UserData.GetValue<List<string>>(UserData.ExamDates).Last<string>();
            await context.PostAsync(StringResources.ExamLoop2(date));
            context.Call(new ExamSubjectLuisDialog(), this.SubjectCompletedAsync);
        }

        private async Task SubjectCompletedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var subject = context.UserData.GetValue<List<string>>(UserData.ExamSubjects).Last<string>();
            await context.PostAsync(StringResources.ExamLoop3(subject));
            await context.PostAsync(StringResources.ExamLoop4);

            context.Call(new ExamPowerLuisDialog(), this.AskUserForLearnDuration);
        }

        private Task AskUserForLearnDuration(IDialogContext context, IAwaitable<object> result)
        {
            PromptDialog.Choice(
               context,
               this.ExamDurationPicked,
               new[] {SelectorConstants.ExamDurationSelector1, SelectorConstants.ExamDurationSelector2, SelectorConstants.ExamDurationSelector3, SelectorConstants.ExamDurationSelector4,
                          SelectorConstants.LunchSelesctor5},
               StringResources.ExamLoop6,
               StringResources.Unkown);

            return Task.CompletedTask;
        }

        private async Task ExamDurationPicked(IDialogContext context, IAwaitable<object> result)
        {

            var selection = await result;

                switch (selection)
                {
                    case SelectorConstants.ExamDurationSelector1:
                         await context.PostAsync(StringResources.ExamLoop5);
                        context.UserData.SetValue(UserData.LunchKey, "10h");
                        break;
                    case SelectorConstants.ExamDurationSelector2:
                        UserDataUtility.AddDurationToUserData(context, selection as string);
                        break;
                    case SelectorConstants.ExamDurationSelector3:
                        UserDataUtility.AddDurationToUserData(context, selection as string);
                        break;
                    case SelectorConstants.ExamDurationSelector4:
                        context.Call(new ExamDurationLuisDialog(), this.ExamDurationLuisFinished);
                        return;
                }
            await AskUserForConfirmation(context);
        }

        private async Task ExamDurationLuisFinished(IDialogContext context, IAwaitable<object> result)
        {
            await AskUserForConfirmation(context);
        }

        private async Task AskUserForConfirmation(IDialogContext context)
        {
            var date = context.UserData.GetValue<List<string>>(UserData.ExamDates).Last();
            var power = context.UserData.GetValue<List<string>>(UserData.ExamPowers).Last();
            var subject = context.UserData.GetValue<List<string>>(UserData.ExamSubjects).Last();
            var duration = context.UserData.GetValue<List<string>>(UserData.ExamDurations).Last();

            PromptDialog.Choice(
             context,
             this.ExamDurationPicked,
             new[] {"Ja", "Nein"},
             StringResources.ExamLoop7(date,power,subject,duration),
             StringResources.Unkown);
        }

        private async Task ExamCompletedConformatation(IDialogContext context, IAwaitable<object> result)
        {

            var selection = await result;

            switch (selection)
            {
                case "Ja":
                    await context.PostAsync(StringResources.ExamLoop8);
                    context.Done(true);
                    return;
                case "Nein":
                    UserDataUtility.RemoveTopFromUserData(context, UserData.ExamDates);
                    UserDataUtility.RemoveTopFromUserData(context, UserData.ExamDurations);
                    UserDataUtility.RemoveTopFromUserData(context, UserData.ExamPowers);
                    UserDataUtility.RemoveTopFromUserData(context, UserData.ExamSubjects);
                    await context.PostAsync(StringResources.ExamLoop9);
                    await this.StartAsync(context);
                    return;
            }
        }


    }
}