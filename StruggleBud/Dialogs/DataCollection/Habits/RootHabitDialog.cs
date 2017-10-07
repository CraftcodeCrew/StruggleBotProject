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
            context.Wait(this.StartBreakfastDataCollectionAsync);

            return Task.CompletedTask;
        }

        private async Task StartBreakfastDataCollectionAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync(StringResources.BreakfastTimeBlockerMessage);

            context.Call(new BreakfastDialog(), this.StartLunchDataCollectionAsync);
        }

        private async Task StartLunchDataCollectionAsync(IDialogContext context, IAwaitable<object> result)
        {
            throw new NotImplementedException();
        }
    }
}