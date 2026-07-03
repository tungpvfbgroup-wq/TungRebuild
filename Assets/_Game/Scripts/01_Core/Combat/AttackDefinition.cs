using System;

namespace Tung.Core.Combat
{
    public enum AttackHitShape
    {
        Circle = 0,
        Cone = 1,
        Raycast = 2
    }
    public readonly struct AttackDefinition
    {
        public float Damage { get; }
        public float Cooldown { get; }
        public float Range { get; }
        public float HitDelay { get; }
        public int MaxTargetCount { get; }
        public int ConeAngleDegrees { get; }
        public AttackHitShape HitShape { get; }
        public AttackDefinition(float damage, float cooldown, float range, float hitDelay, int maxTargetCount, int coneAngleDegrees, AttackHitShape hitShape)
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
            if (maxTargetCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxTargetCount));
            }
            if (hitShape == AttackHitShape.Cone && (coneAngleDegrees <= 0 || coneAngleDegrees > 180))
            {
                throw new ArgumentOutOfRangeException(nameof(coneAngleDegrees));
            }
            Damage = damage;
            Cooldown = cooldown;
            Range = range;
            HitDelay = hitDelay;
            MaxTargetCount = maxTargetCount;
            ConeAngleDegrees = hitShape == AttackHitShape.Cone ? coneAngleDegrees : 0;
            HitShape = hitShape;
        }


    }
}