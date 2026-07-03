using Tung.Core.Combat;
using Tung.Modules.Player.Domain;
using UnityEngine;

namespace Tung.Modules.Player.Infrastructure.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Tung/Player/PlayerConfig")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [field: SerializeField, Min(0.01f)] public float MaxHealth { get; private set; } = 10f;
        [field: SerializeField, Min(0.01f)] public float MoveSpeed { get; private set; } = 5f;
        [field: SerializeField, Min(1)] public int MaxDurability { get; private set; } = 100;
        [field: SerializeField, Min(0.01f)] public float Damage { get; private set; } = 2f;
        [field: SerializeField, Min(0f)] public float Cooldown { get; private set; } = 0.2f;
        [field: SerializeField, Min(0f)] public float Range { get; private set; } = 0.2f;
        [field: SerializeField, Min(0f)] public float HitDelay { get; private set; } = 0.2f;
        [field: SerializeField, Min(1)] public int MaxTargetCount { get; private set; } = 1;
        [field: SerializeField, Range(0, 180)] public int ConeAngleDegrees { get; private set; } = 90;
        [field: SerializeField] public AttackHitShape HitShape { get; private set; } = AttackHitShape.Cone;
        public PlayerDefinition ToDefinition()
        {
            return new PlayerDefinition(
                MaxHealth, MoveSpeed,
                new WeaponDefinition(
                    MaxDurability,
                    new AttackDefinition(Damage, Cooldown, Range, HitDelay, MaxTargetCount,
                    ConeAngleDegrees, HitShape)));
        }
    }

}