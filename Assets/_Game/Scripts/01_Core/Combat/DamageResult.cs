namespace Tung.Core.Combat
{
    public readonly struct DamageResult
    {
        public float AppliedDamage { get; }
        public float RemainingHealth { get; }
        public bool JustDied { get; }
        public DamageResult(float appliedDamage, float remaniningHealth, bool justDied)
        {
            AppliedDamage = appliedDamage;
            RemainingHealth = remaniningHealth;
            JustDied = justDied;
        }
    }
}