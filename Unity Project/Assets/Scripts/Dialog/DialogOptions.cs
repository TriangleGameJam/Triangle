using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Dialog
{
    /// <summary>
    /// First item in the list is the enemy's dialog, the next 3 are the player options
    /// </summary>
    public static class DialogOptions
    {
        public static List<string> SisterDialog()
        {
            return new List<string>
            {
                "BUM! I told you to go get the dishes. You make me so angry. What do you have to say for yourself?",
                "Lemme just finish this bottle of beer...",
                "I don't care...",
                "Do them yourself..."
            };
        }

        public static List<string> DadDialog()
        {
            return new List<string>
            {
                "Son, I need you to fix my workout equipment.",
                "You're already big enough, dad...",
                "I wish I cared enough to do it...",
                "I'm playing CS, I'll do it tomorrow..."
            };
        }

        public static List<string> MomDialog()
        {
            return new List<string>
            {
                "Honey, could you be a dear and help mommy curl her hair?",
                "Your hair is ugly either way, mom...",
                "Not now, I need to catch up to Naruto...",
                "I'm too tired, I'll do it later..."
            };
        }
    }
}
