using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using StruggleBud.Resources;
using StruggleBud.Dialogs.Habits;

namespace StruggleBud.Dialogs.DataCollection
{
    [Serializable]
    public class RootHabitDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Call(new BreakfastDialog(), this.StartLunchDataCollectionAsync);
            return Task.CompletedTask;
        }

        private Task StartLunchDataCollectionAsync(IDialogContext context, IAwaitable<object> result)
        {
            context.Call(new LunchDialog(), this.StartDinnerDataCollectionAsync);
            return Task.CompletedTask;
        }

        private async Task StartDinnerDataCollectionAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync(StringResources.DinnerWelcomeMessage);

            context.Call(new DinnerDialog(), this.StartSleepScheduleDataCollectionAsync);
        }

        private async Task StartSleepScheduleDataCollectionAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync(StringResources.SleepScheduleWelcomeMessage);

            context.Call(new SleepScheduleDialog(), this.EndOfHabitDataCollectionAsync);
        }

        private Task EndOfHabitDataCollectionAsync(IDialogContext context, IAwaitable<object> result)
        {
            context.Done(true);
            return Task.CompletedTask;
        }
    }
}