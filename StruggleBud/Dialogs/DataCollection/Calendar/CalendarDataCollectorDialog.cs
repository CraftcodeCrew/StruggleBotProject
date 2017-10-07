using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

// Navigates back to RootDataCollectionDialog
namespace StruggleBud.Dialogs.DataCollection.Calendar
{
    [Serializable]
    public class CalendarDataCollectorDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            throw new NotImplementedException();
        }
    }
}