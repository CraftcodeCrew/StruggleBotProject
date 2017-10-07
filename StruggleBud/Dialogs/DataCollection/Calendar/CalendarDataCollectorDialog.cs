using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

// Navigates back to RootDataCollectionDialog
namespace StruggleBud.Dialogs.DataCollection.Calendar
{
    using StruggleBud.Resources;

    [Serializable]
    public class CalendarDataCollectorDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.PostAsync(StringResources.CalenderAccess);

            var calenderApiAccess = true;

            if (calenderApiAccess == false)
            {
                context.PostAsync(StringResources.CalenderAccessFailed);
                await this.StartAsync(context);
            }
            else
            {
                context.Done(true);
            }
        }
    }
}