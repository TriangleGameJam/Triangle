using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Dialog
{
    public static class DialogOptions
    {
        public static List<string> SisterDialog()
        {
            List<string> op1Ans = new List<string>
            {
                "BUM! I told you to go get the dishes. You make me so angry. What do you have to say for yourself?",
                "I'm so sorry sis :(...",
                "I don't care...",
                "DO THEM YOURSELF..."
            };
            return op1Ans;
        }

    }
}
