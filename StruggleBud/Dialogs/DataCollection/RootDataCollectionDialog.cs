﻿using Microsoft.Bot.Builder.Dialogs;
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
    public class RootDataCollectionDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(this.StartHabitDataCollectionAsync);

            return Task.CompletedTask;
        }

        private async Task StartHabitDataCollectionAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync(StringResources.TimeBlockerMessage1);

            context.Call(new RootHabitDialog(), this.StartLunchDataCollectionAsync);
        }

        private Task StartLunchDataCollectionAsync(IDialogContext context, IAwaitable<object> result)
        {
            throw new NotImplementedException();
        }
    }
}