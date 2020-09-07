using System;

namespace NetCoreLibrary.Domain.Organizations
{
    public class Organization : IEquatable<Organization>
    {
        public Organization(string name)
        {
            Identifier = new OrganizationIdentifier();
            Name = new OrganizationName(name);
            IsEnabled = true;
        }

        public Organization(Guid organizationId, string name, bool isEnabled)
        {
            Identifier = new OrganizationIdentifier(organizationId);
            Name = new OrganizationName(name);
            IsEnabled = isEnabled;
        }

        public void UpdateName(string name)
        {
            Name = new OrganizationName(name);
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        public void Disable()
        {
            IsEnabled = false;
        }
        
        public OrganizationIdentifier Identifier { get; }
        public OrganizationName Name { get; private set; }
        public bool IsEnabled { get; private set; }

        public bool Equals(Organization other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Identifier, other.Identifier);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Organization) obj);
        }

        public override int GetHashCode()
        {
            return (Identifier != null ? Identifier.GetHashCode() : 0);
        }

        public static bool operator ==(Organization left, Organization right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Organization left, Organization right)
        {
            return !Equals(left, right);
        }
    }
}