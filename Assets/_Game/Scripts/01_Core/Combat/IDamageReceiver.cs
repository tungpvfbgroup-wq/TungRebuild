namespace Tung.Core.Combat
{
    public interface IDamageReceiver
    {
        DamageResult ReceiveDamage(DamageInfo damageInfo);
    }

}