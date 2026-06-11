using System;
namespace Tung.Core.ValueObjects
{
    public readonly struct TungEntityId : IEquatable<TungEntityId>
    {
        private readonly Guid _value;
        private TungEntityId(Guid value) { _value = value; }
        public static TungEntityId New() => new TungEntityId(Guid.NewGuid());
        public static TungEntityId Invalid => default;
        public bool IsValid => _value != Guid.Empty;
        public bool Equals(TungEntityId other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is TungEntityId other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();
        public override string ToString() => IsValid ? _value.ToString("N")[..8] : "Invalid";
        public static bool operator ==(TungEntityId left, TungEntityId right) => left.Equals(right);
        public static bool operator !=(TungEntityId left, TungEntityId right) => !left.Equals(right);
    }
}