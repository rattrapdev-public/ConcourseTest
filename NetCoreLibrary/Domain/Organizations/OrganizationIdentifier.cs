using System;

namespace NetCoreLibrary.Domain.Organizations
{
    public class OrganizationIdentifier : IEquatable<OrganizationIdentifier>
    {
        public OrganizationIdentifier() : this(Guid.NewGuid()) {}
        
        public OrganizationIdentifier(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("The id must not be empty");
            }

            Id = id;
        }
        
        public Guid Id { get; }

        public bool Equals(OrganizationIdentifier other)
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
            return Equals((OrganizationIdentifier) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}