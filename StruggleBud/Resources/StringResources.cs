﻿using System;
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



        public static string TimeBlockerMessage1() => "Du willst ja nicht immer lernen, oder? Lass uns zunächst deine täglichen Mahlzeiten eintragen.";
        public static string BreakfastTimeBlockerMessage() => "Das Frühstück ist die wichtigste Mahlzeit des Tages. Um wieviel Uhr frühstückst du normalerweise?";
        public static string InvalidBreakfastMessage() => "Das habe ich nicht ganz verstanden. Schreib am Besten die Uhrzeit in dem Format hh:mm, oder nutze einfach eine unserer Antwortmöglichkeiten.";
    }
}