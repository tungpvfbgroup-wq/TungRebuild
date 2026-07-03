using System;

namespace Tung.Core.Combat
{
    public readonly struct WeaponDefinition
    {
        public int MaxDurability { get; }
        public AttackDefinition Attack { get; }
        public WeaponDefinition(int maxDurability, AttackDefinition attack)
        {
            if (maxDurability <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxDurability));
            }
            if (attack.Damage <= 0f)
            {
                throw new ArgumentException(nameof(attack));
            }
            MaxDurability = maxDurability;
            Attack = attack;
        }

    }
}