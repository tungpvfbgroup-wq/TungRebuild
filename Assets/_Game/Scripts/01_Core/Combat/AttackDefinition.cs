using System;

namespace Tung.Core.Combat
{
    public readonly struct AttackDefinition
    {
        public float Damage { get; }
        public float Cooldown { get; }
        public float Range { get; }
        public float HitDelay { get; }
        public int MaxTargetCount { get; }
        public int ConeAngleDegrees { get; }
        public AttackDefinition(float damage, float cooldown, float range, float hitDelay,
        int maxTargetCount, int coneAngleDegrees)
        {
            if (damage <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(damage));
            }
            if (cooldown < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(cooldown));
            }
            if (range < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(range));
            }
            if (hitDelay < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(hitDelay));
            }
            if (maxTargetCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(maxTargetCount));
            }
            Damage = damage;
            Cooldown = cooldown;
            Range = range;
            HitDelay = hitDelay;
            MaxTargetCount = maxTargetCount;
            ConeAngleDegrees = coneAngleDegrees;
        }


    }
}