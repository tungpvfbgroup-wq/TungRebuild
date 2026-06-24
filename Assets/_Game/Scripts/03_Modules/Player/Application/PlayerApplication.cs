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
        private readonly DamageInfo _damageInfo;
        public Action<TungEntityId, RewardBundle> DieCallBack { get; set; }
        public PlayerApplication(TungEntityId entityId, PlayerState state, PlayerDefinition definition)
        {
            if (!entityId.IsValid)
            {
                throw new ArgumentNullException(nameof(entityId));
            }
            _entityId = entityId;
            _state = state;
            _definition = definition;
        }
        public void ComputeMoveVelocity(float inputX, float inputY, out float velocityX, out float velocityY)
        {
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
        public DamageResult ReceiveDamage(DamageInfo damageInfo)
        {
            var requestedDamage = damageInfo.Amount;
            if (requestedDamage < 0f)
            {
                requestedDamage = 0f;
            }
            var wasAlive = !_state.IsDead;
            var appliedDamage = 0f;
            if (wasAlive)
            {
                appliedDamage = requestedDamage;
                if (appliedDamage > _state.CurrentHealth)
                {
                    appliedDamage = _state.CurrentHealth;
                }
                var nextHealth = _state.CurrentHealth - appliedDamage;
                _state.SetCurrentHealth(nextHealth);
            }
            var justDied = wasAlive && _state.IsDead;
            if (justDied)
            {
                DieCallBack?.Invoke(_entityId, new RewardBundle(0, 0));
            }
            return new DamageResult(appliedDamage, _state.CurrentHealth, justDied);
        }
    }
}