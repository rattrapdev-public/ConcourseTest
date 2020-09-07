using System;

namespace NetCoreLibrary.Domain.Users
{
    public class UserIdentifier : IEquatable<UserIdentifier>
    {
        public UserIdentifier()
            : this(Guid.NewGuid())
        {
            
        }
        
        public UserIdentifier(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty");
            }
            
            Id = id;
        }
        
        public Guid Id { get; }

        public bool Equals(UserIdentifier other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UserIdentifier) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(UserIdentifier left, UserIdentifier right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UserIdentifier left, UserIdentifier right)
        {
            return !Equals(left, right);
        }
    }
}