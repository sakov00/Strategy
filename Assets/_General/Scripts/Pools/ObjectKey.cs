using System;

namespace _General.Scripts.Pools
{
    public struct ObjectKey : IEquatable<ObjectKey>
    {
        public Type ObjectType;
        public string SubType;

        public ObjectKey(Type type, string subType)
        {
            ObjectType = type;
            SubType = subType;
        }

        public bool Equals(ObjectKey other) => ObjectType == other.ObjectType && SubType == other.SubType;
        public override bool Equals(object obj) => obj is ObjectKey other && Equals(other);
        public override int GetHashCode() => ObjectType.GetHashCode() ^ SubType.GetHashCode();
    }
}