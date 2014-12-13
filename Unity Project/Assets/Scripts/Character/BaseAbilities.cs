using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Character
{
    public abstract class BaseCombos
    {

    }

    public abstract class OffensiveCombos : BaseCombos
    {
        public string KeySequence;
        public abstract void DoDamage();
    }

    public abstract class DefensiveCombos : BaseCombos
    {
        public string KeySequence;
        public abstract void Defend();
    }
}
