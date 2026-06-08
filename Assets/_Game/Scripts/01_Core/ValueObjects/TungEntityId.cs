
using System;

namespace Tung.Core.ValueObjects
{
    public readonly struct TungEntityId : IEquatable<TungEntityId> // là một struct không thể thay đổi (immutable) và có thể so sánh với nhau
    {
        private readonly Guid _maDinhDanh; // một trường riêng tư kiểu Guid để lưu trữ giá trị của TungEntityId
        private TungEntityId(Guid maDinhDanh) // một constructor riêng tư để khởi tạo giá trị của TungEntityId
        {
            _maDinhDanh = maDinhDanh;
        }   

        public bool Equals(TungEntityId other)
        {
            return _maDinhDanh == other._maDinhDanh; // so sánh giá trị của hai TungEntityId bằng cách so sánh trường _maDinhDanh của chúng
        }
    }
}
