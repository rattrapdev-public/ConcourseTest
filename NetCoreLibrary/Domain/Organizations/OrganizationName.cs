using System;

namespace NetCoreLibrary.Domain.Organizations
{
    public class OrganizationName : IEquatable<OrganizationName>
    {
        public OrganizationName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name must be a valid string");
            }

            Name = name;
        }
        
        public string Name { get; }

        public bool Equals(OrganizationName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OrganizationName) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}