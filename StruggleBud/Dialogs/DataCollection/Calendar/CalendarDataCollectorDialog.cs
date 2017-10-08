using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

// Navigates back to RootDataCollectionDialog
namespace StruggleBud.Dialogs.DataCollection.Calendar
{
    using Microsoft.Bot.Connector;
    using StruggleApplication.framework;
    using StruggleBud.Resources;

    [Serializable]
    public class CalendarDataCollectorDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync(StringResources.CalenderAccess);

            var apiAccessPoint = new GoogleCalendarInstance();
            var link = apiAccessPoint.GenerateAuthLink();
            context.UserData.SetValue<GoogleCalendarInstance>(UserData.UserCalenderToken, apiAccessPoint);

            await context.PostAsync(link);

            context.Wait(this.AuthLinkRecevied);

        }

        private async Task AuthLinkRecevied(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            var apiAccessPoint = context.UserData.GetValue<GoogleCalendarInstance>(UserData.UserCalenderToken);

            await apiAccessPoint.Initialize(activity.Text);
            context.Done(true);

        }
    }
}