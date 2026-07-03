
namespace Tung.Core.Combat
{
    public enum AttackBlockReason
    {
        Dead = 0,
        Cooldown = 1,
        BrokenWeapon = 2
    }
    public readonly struct AttackStartResult
    {
        public bool Started { get; }
        public AttackRequest Request { get; }
        public AttackBlockReason? BlockReason { get; }
        private AttackStartResult(bool started, AttackRequest request, AttackBlockReason? blockReason)
        {
            Started = started;
            BlockReason = blockReason;
            Request = request;
        }
        public static AttackStartResult CreateStarted(AttackRequest request)
        {
            return new AttackStartResult(true, request, null);
        }
        public static AttackStartResult CreateBlocked(AttackBlockReason blockreason)
        {
            return new AttackStartResult(false, default, blockreason);
        }



    }

}