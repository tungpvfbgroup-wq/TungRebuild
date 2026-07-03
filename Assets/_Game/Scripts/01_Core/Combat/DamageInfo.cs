using System;
using Tung.Core.ValueObjects;

namespace Tung.Core.Combat
{// `DamageInfo` = gói damage thực sự gửi sang target ở hit frame.
    public readonly struct DamageInfo
    {
        public TungEntityId SourceId { get; }  // `SourceId` = ai là chủ thể gây ra hit này.
        public float Amount { get; }// `Amount` = lượng damage gốc được gửi sang target trước khi target tự xử lý giáp / clamp máu.
        public DamageInfo(TungEntityId entityId, float amount)
        {
            if (!entityId.IsValid)
            {
                throw new ArgumentException(nameof(entityId));
            }
            if (amount <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }
            SourceId = entityId;
            Amount = amount;
        }
    }
}