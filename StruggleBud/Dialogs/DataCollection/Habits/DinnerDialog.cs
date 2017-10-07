using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StruggleBud.Dialogs.Habits
{
    using System.Threading.Tasks;

    using Microsoft.Bot.Builder.Dialogs;

    using StruggleBud.Resources;

    public class DinnerDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            PromptDialog.Choice(
                context,
                null,
                new[] {SelectorConstants.LunchSelesctor1, SelectorConstants.LunchSelesctor2, SelectorConstants.LunchSelesctor3, SelectorConstants.LunchSelesctor4,
                          SelectorConstants.LunchSelesctor5},
                StringResources.LaunchWelcomeMessage,
                StringResources.Unkown);

            return Task.CompletedTask;
        }
    }
}