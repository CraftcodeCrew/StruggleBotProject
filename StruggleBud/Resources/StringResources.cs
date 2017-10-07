using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StruggleBud.Resources
{
    public class StringResources
    {
        public const string WelcomeMessage1 = "Hi! Ich freue mich, dass du zu mir gefunden hast. Ich bin dein StruggleBud und unterstütze dich bei deiner persönlichen Lernvorbereitung.";
        public const string WelcomeMessage2 = "Damit ich dich in Zukunft richtig anspreche brauche ich deinen Namen. Wie heißt du?";
        public static string WelcomeMessage3(string name) => $"Hallo, {name} ich habe noch keinen Studienplan von für dich. Soll ich einen anlegen?";

        public const string Unkown = "Sorry, das habe ich nicht verstanden";

        public const string CreationAboard1 = "Wie kann ich dir sonst weiterhelfen?";
        public const string CreationAboard2 = "Kannste knicken! Willst du jetzt einen Studienplan anlegen?";

        public const string CalenderAccess =
                "Damit ich dir wirklich helfen kann, benötige ich Zugriff auf deinen Kalender. Klicke dafür einfach auf folgenden Link und erteile mir deine Erlaubnis"
            ;
        public const string CalenderAccessFailed = "Irgendwas ist schiefgelaufen. Kannst du es normal versuchen?"
            ;

        public const string TimeBlockerMessage1 = "Du willst ja nicht immer lernen, oder? Lass uns zunächst deine täglichen Mahlzeiten eintragen.";
        public const string BreakfastTimeBlockerMessage = "Das Frühstück ist die wichtigste Mahlzeit des Tages. Um wie viel Uhr frühstückst du normalerweise?";
        public const string InvalidBreakfastMessage = "Das habe ich nicht ganz verstanden. Schreib am Besten die Uhrzeit in dem Format hh:mm, oder nutze einfach eine unserer Antwortmöglichkeiten.";
        public const string NoBreakfastMessage = "Alles klar, dann halt nicht...das spart ordentlich Zeit. Willst du wirklich nichts frühstücken?";
        public static string BreakfastTimeSetMessage(string time) => $"Der frühe Vogel fängt den Wurm. Ich blocke dir 1 Stunde ab {time} für dein Frühstück. Ist das OK so?";
        public const string BreakfastConfirmationMessage = "Top! Weiter geht’s :-)";


        public const string LaunchWelcomeMessage = "Morgens wie ein Kaiser, mittags wie ein König, abends wie ein Bauer. Zu welcher Uhrzeit isst du dein Mittagessen?";
        public const string SleepWelcomeMessage = "Erstmal schlafen, aber wann ?!";

        public const string LunchDoneMessage = "Top! Weiter geht’s :-)";

        public static string LunchConfirmationMessage(string time) =>
            $"Danke, ich notiere dir deine Mittagspause für {time}. Ist das OK so?";

        public static string DinnerConfirmationMessage(string time) =>
            $"Ich trage dir dein Abendessen für {time} ein. Ist das OK so?";

        public static string SleepConfirmationMessage(string time) =>
            $" Damit du auch täglich frisch in den Tag startest, plane ich für dich eine ausgelassene Schlafenszeit von 8 Stunden ein. Gehst du um {time} wirklich ins Bett?";

        public const string DinnerNoMessage = "Schade... Ich hätte gerne mit dir zu Abend gegessen";

        public const string DinnerDoneMessage = "Top! Weiter geht’s :-)";
        public const string SleepDoneMessage = "Top! Weiter geht’s :-)";



        public const string NoLunchMessage = "Dude sicher... Essen ist wichtig?";
        public const string NoSleepMessage = "Das kann ich leider nicht durchgehen lassen! Wer schaffen will, muss auch schlafen.";


        public const string DinnerWelcomeMessage = "Für wann soll ich dir dein Abendessen am Besten einplanen?";

        public const string SleepScheduleWelcomeMessage = "Nach getaner Arbeit freust du dich auf den wohlverdienten Schlaf. Um wie viel Uhr gehst du denn normalerweise zu Bett?";


        public const string NoTime = "Alles klar, dann halt nicht...das spart ordentlich Zeit";

        public static string FinishWelcomeMessage(string name) =>
            $"Okay {name}, wir haben den nervigen Teil Erledigt!. Hier nocheinmal eine Zusammenfassung:";


        public const string ExamWelcomeMessage = "Wollen wir zusammen deine Lernzeiten planen?";
        public const string ExamLoopBeginMessage = "Dann können wir ja starten. Lass uns durch deine Prüfungen gehen.";

        public const string ExamLoop1 = "Wann ist deine Prüfung?";
        public static string ExamLoop2(string date) => $"Ok am {date}. Für welches Fach oder mit welchem Thema?";
        public static string ExamLoop3(string examTopic) => $"Ok du schreibst {examTopic}.";
        public const string ExamLoop4 = "Circa wieviel Stunden solltest du dafür insgesamt lernen?";
        public const string ExamLoop5 = "Das sollte passen!";

        public const string ExamLoop6 = "Ich nehme an dein Lerntempo ist 10 Stunden pro Woche. Oder wie viel ist dir lieber?";

        public static string ExamLoop7(string date, string power, string subject, string duration) => $"Also du hast am {date} deine {subject}-Prüfung, dafür solltest du circa {power}h Stunden lernen und willst dich mit {duration} Stunden pro Woche darauf vorbereiten. Richtig?";

        public const string ExamLoop8 = "Okay das merke ich mir!";

        public const string ExamLoop9 = "Dude!, dann eben nochmal von vorne!";

        public const string ExamReenterLoop = "Hast du noch mehr Prüfungen, von denen du mir erzählen solltest?";

        public const string FallbackSkio = "Okay mate! Versuche es nocheinmal";

        public const string FallbackMessage = "Mate, das ist übel... Willst du es vlt. nochmal versuchen";
    }
}