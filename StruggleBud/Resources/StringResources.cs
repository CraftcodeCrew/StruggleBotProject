using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StruggleBud.Resources
{
    public class StringResources
    {
        public const string WelcomeMessage1 =
                "Hi! Ich freue mich, dass du zu mir gefunden hast. Ich bin dein StruggleBud und unterstütze dich bei deiner persönlichen Lernvorbereitung."
           ;

        public const string WelcomeMessage2 = "Damit ich dich in Zukunft richtig anspreche brauche ich deinen Namen. Wie heißt du?"
            ;

        public static string WelcomeMessage3(string name) => $"Hallo, {name} ch habe noch keinen Studienplan von dirfür dich. Soll ich einen anlegen?";

        public const string Unkown = "Sorry, das habe ich nicht verstanden"
            ;

        public const string CreationAboard1 = "Wie kann ich dir sonst weiterhelfen?";

        public const string CreationAboard2 = "Kannste knicken! Willst du jetzt einen Studienplan anlegen?";

        public const string CalenderAccess =
                "Damit ich dir wirklich helfen kann, benötige ich Zugriff auf deinen Kalender. Klicke  dafür einfach auf folgenden Link und erteile mir deine Erlaubnis"
            ;
        public const string CalenderAccessFailed = "Irgendwas ist schiefgelaufen. Kannst du es normal versuchen?"
            ;

        public const string TimeBlockerMessage1 = "Du willst ja nicht immer lernen, oder? Lass uns zunächst deine täglichen Mahlzeiten eintragen.";
        public const string BreakfastTimeBlockerMessage = "Das Frühstück ist die wichtigste Mahlzeit des Tages. Um wieviel Uhr frühstückst du normalerweise?";
        public const string InvalidBreakfastMessage = "Das habe ich nicht ganz verstanden. Schreib am Besten die Uhrzeit in dem Format hh:mm, oder nutze einfach eine unserer Antwortmöglichkeiten.";
        public const string NoBreakfastMessage = "Alles klar, dann halt nicht...das spart ordentlich Zeit. Willst du wirklich nichts frühstücken?";
        public static string BreakfastTimeSetMessage(string time) => $"Der frühe Vogel fängt den Wurm. Ich blocke dir 1 Stunde ab {time} Uhr für dein Frühstück.Ist das OK so?";
        public const string BreakfastConfirmationMessage = "Top! Weiter geht’s :-)";


        public const string LaunchWelcomeMessage = "Morgens wie ein Kaiser, mittags wie ein König, abends wie ein Bauer. Zu welcher Uhrzeit isst du dein Mittagessen?";

        public const string LunchDoneMessage = "Top! Weiter geht’s :-)";

        public static string LunchConfirmationMessage(string time) =>
            $"Danke, ich notiere dir deine Mittagspause für {time} Uhr. Ist das OK so?";

        public const string NoLunchMessage = "Dude sicher... Essen ist wichtig?";


        public const string DinnerWelcomeMessage = "Für wann soll ich dir dein Abendessen am Besten einplanen?";

        public const string SleepScheduleWelcomeMessage = "Nach getaner Arbeit freust du dich auf den wohlverdienten Schlaf. Um wie viel Uhr gehst du denn normalerweise zu Bett?";

        public const string NoTime = "Alles klar, dann halt nicht...das spart ordentlich Zeit";
    }
}