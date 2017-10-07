using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace StruggleBud.Dialogs.Habits
{
    [Serializable]
    public class SleepScheduleDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            return Task.CompletedTask;
        }
    }
}