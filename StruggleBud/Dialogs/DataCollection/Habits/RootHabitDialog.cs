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
            context.Done(true);
            return Task.CompletedTask;
        }
    }
}