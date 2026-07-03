using System;
using Tung.Core.Combat;

namespace Tung.Modules.Player.Domain
{
    public sealed class PlayerDefinition
    {
        public float MaxHealth { get; }
        public float MoveSpeed { get; }
        public WeaponDefinition StartWeapon { get; }
        public PlayerDefinition(float maxHealth, float moveSpeed, WeaponDefinition startWeapon)
        {
            if (maxHealth <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(maxHealth), "PlayerDefinition requires maxHealth > 0");
            }
            if (moveSpeed <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(moveSpeed), "PlayerDefinition requires moveSpeed > 0");
            }
            if (startWeapon.MaxDurability <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startWeapon), "PlayerDefinition requires a valid WeaponDefinition");
            }
            MaxHealth = maxHealth;
            MoveSpeed = moveSpeed;
            StartWeapon = startWeapon;
        }
    }
}
