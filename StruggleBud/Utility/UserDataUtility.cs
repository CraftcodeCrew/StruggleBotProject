using Microsoft.Bot.Builder.Dialogs;
using StruggleBud.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StruggleBud.Utility
{
    public class UserDataUtility
    {
        public static void AddDurationToUserData(IDialogContext context, string duration)
        {
            
            var list = new List<string>();

            try
            {
                list = context.UserData.GetValue<List<string>>(UserData.ExamDates);
            }
            catch (Exception)
            {

            }

            list.Add(duration);

            context.UserData.SetValue<List<string>>(UserData.ExamDurations, list);
        }

        public static void RemoveTopFromUserData(IDialogContext context, string key)
        {
            var list = context.UserData.GetValue<List<string>>(key);

            if (list != null)
            {
                list.RemoveAt(list.Count - 1);
                context.UserData.SetValue<List<string>>(key, list);
            }


        }
    }
}