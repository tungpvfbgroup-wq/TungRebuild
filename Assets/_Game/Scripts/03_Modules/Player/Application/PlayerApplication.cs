using System;
using Tung.Core.Combat;
using Tung.Core.Rewards;
using Tung.Core.ValueObjects;
using Tung.Modules.Player.Domain;
namespace Tung.Modules.Player.Application
{
    public sealed class PlayerApplication : IDamageReceiver
    {
        private readonly TungEntityId _entityId;
        private readonly PlayerState _state;
        private readonly PlayerDefinition _definition;
        public bool IsDead => _state.IsDead;
        public WeaponDefinition CurrentWeapon => _state.CurrentWeapon;
        public event Action<TungEntityId, RewardBundle> Die;
        public PlayerApplication(TungEntityId entityId, PlayerState state, PlayerDefinition definition)
        {
            if (!entityId.IsValid)
            {
                throw new ArgumentNullException(nameof(entityId));
            }
            _entityId = entityId;
            _state = state ?? throw new ArgumentNullException(nameof(state));
            _definition = definition ?? throw new ArgumentNullException(nameof(definition));
        }
        public void ComputeMoveVelocity(float inputX, float inputY, out float velocityX, out float velocityY)
        {
            if (_state.IsDead)
            {
                velocityX = 0f;
                velocityY = 0f;
                _state.SetMoveVelocity(velocityX, velocityY);
                return;
            }
            var magnitudeSquared = (inputX * inputX) + (inputY * inputY);
            if (magnitudeSquared > 1f)
            {
                var magnitude = MathF.Sqrt(magnitudeSquared);
                inputX /= magnitude;
                inputY /= magnitude;
            }
            velocityX = inputX * _definition.MoveSpeed;
            velocityY = inputY * _definition.MoveSpeed;
            _state.SetMoveVelocity(velocityX, velocityY);
        }
        public PlayerReadModel GetReadModel()
        {
            return new PlayerReadModel(_state.MaxHealth, _state.CurrentHealth, _state.MoveVelocityX,
            _state.MoveVelocityY, _state.MaxWeaponDurability, _state.CurrentWeaponDurability,
            _state.IsMoving, _state.IsDead);
        }
        public void ConsumeWeaponDurability(int amount)
        {
            _state.ConsumeWeaponDurability(amount);
        }
        public AttackStartResult TryStartAttack(float currentTime)
        {
            if (_state.IsDead)
            {
                return AttackStartResult.CreateBlocked(AttackBlockReason.Dead);
            }
            if (currentTime < _state.NextAttackTime)
            {
                return AttackStartResult.CreateBlocked(AttackBlockReason.Cooldown);
            }
            if (_state.CurrentWeaponDurability <= 0)
            {
                return AttackStartResult.CreateBlocked(AttackBlockReason.BrokenWeapon);
            }
            var attack = _state.CurrentWeapon.Attack;
            _state.SetNextAttackTime(currentTime + attack.Cooldown);
            return AttackStartResult.CreateStarted(new AttackRequest(_entityId, attack, currentTime));
        }
        public DamageResult ReceiveDamage(DamageInfo damageInfo)
        {
            if (_state.IsDead)
            {
                return new DamageResult(0f, _state.CurrentHealth, false);
            }

            var requestedDamage = damageInfo.Amount;
            if (requestedDamage < 0f)
            {
                requestedDamage = 0f;
            }

            var appliedDamage = requestedDamage;
            if (appliedDamage > _state.CurrentHealth)
            {
                appliedDamage = _state.CurrentHealth;
            }
            var nextHealth = _state.CurrentHealth - appliedDamage;
            _state.SetCurrentHealth(nextHealth);
            var justDied = _state.IsDead;
            if (justDied)
            {
                Die?.Invoke(_entityId, new RewardBundle(0, 0));
            }
            return new DamageResult(appliedDamage, _state.CurrentHealth, justDied);
        }
    }
}