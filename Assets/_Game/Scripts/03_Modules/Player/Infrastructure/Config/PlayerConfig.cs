using Tung.Modules.Player.Domain;
using UnityEngine;

namespace Tung.Modules.Player.Infrastructure.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Tung/Player/PlayerConfig")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [field: SerializeField, Min(0.01f)] public float MaxHealth { get; private set; } = 10f;
        [field: SerializeField, Min(0.01f)] public float MoveSpeed { get; private set; } = 5f;
        [field: SerializeField, Min(0f)] public float AttackDamage { get; private set; } = 2f;
        [field: SerializeField, Min(0f)] public float AttackCooldown { get; private set; } = 0.2f;
        public PlayerDefinition ToDefinition()
        {
            return new PlayerDefinition(MaxHealth, MoveSpeed, AttackDamage, AttackCooldown);
        }
    }

}