using Tung.Core.ValueObjects;

namespace Tung.Core.Combat
{
    public readonly struct DamageInfo
    {
        public TungEntityId SourceId { get; }
        public float Amount { get; }
        public DamageInfo(TungEntityId entityId, float amount)
        {
            SourceId = entityId;
            Amount = amount;
        }
    }
}