using System;

namespace Assets.Scripts.Character
{
    public abstract class BaseCombos
    {
        public readonly int id;

        public BaseCombos() { }

        public BaseCombos(int id)
        {
            this.id = id;
        }
    }

    public abstract class OffensiveCombos : BaseCombos
    {
        public readonly float damage;

        public OffensiveCombos() { }

        public OffensiveCombos(float damage)
        {
            this.damage = damage;
        }
    }

    public abstract class DefensiveCombos : BaseCombos
    {
        public readonly float block;

        public DefensiveCombos() { }

        public DefensiveCombos(float block)
        {
            this.block = block;
        }
    }

    public abstract class Buff : BaseCombos
    {

    }

    public abstract class MeleeAttack : OffensiveCombos
    {

    }

    public abstract class RangedAttack : OffensiveCombos
    {

    }

    public abstract class Dodge : DefensiveCombos
    {

    }

    public abstract class Block : DefensiveCombos
    {

    }
}
