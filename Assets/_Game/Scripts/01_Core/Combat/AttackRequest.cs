using System;
using Tung.Core.ValueObjects;
namespace Tung.Core.Combat
{
    public readonly struct AttackRequest
    {
        public TungEntityId AttackerId { get; }
        public AttackDefinition Attack { get; }
        public float StartedAtTime { get; }
        public float HitTime => StartedAtTime + Attack.HitDelay;
        public AttackRequest(TungEntityId attackerId, AttackDefinition attack, float startedAtTime)
        {
            if (!attackerId.IsValid)
            {
                throw new ArgumentException(nameof(attackerId));
            }
            if (attack.Damage <= 0f)
            {
                throw new ArgumentException(nameof(attack), "AttackRequest requires a valid AttackDefinition");
            }
            if (startedAtTime < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(startedAtTime));
            }
            AttackerId = attackerId;
            Attack = attack;
            StartedAtTime = startedAtTime;
        }
    }
}