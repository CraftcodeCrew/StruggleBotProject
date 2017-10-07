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
    }
}