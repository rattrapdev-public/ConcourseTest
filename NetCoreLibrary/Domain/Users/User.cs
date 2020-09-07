using System;
using NetCoreLibrary.Domain.Organizations;

namespace NetCoreLibrary.Domain.Users
{
    public class User
    {
        public User(string username, string email, string password, Guid organizationId)
        {
            Identifier = new UserIdentifier();
            Credentials = new Credentials(username, email);
            Password = new Password(password);
            OrganizationIdentifier = new OrganizationIdentifier(organizationId);
        }

        public User(Guid userId, string username, string email, string password, Guid organizationId)
        {
            Identifier = new UserIdentifier(userId);
            Credentials = new Credentials(username, email);
            Password = new Password(password, true);
            OrganizationIdentifier = new OrganizationIdentifier(organizationId);
        }

        public void UpdateCredentials(string username, string email)
        {
            Credentials = new Credentials(username, email);
        }

        public void ResetPassword(string oldPassword, string newPassword)
        {
            var hashedOldPassword = new Password(oldPassword);
            if (!hashedOldPassword.Equals(Password))
            {
                throw new ArgumentException("The old password must match the current password");
            }
            
            Password = new Password(newPassword);
        }
        
        public UserIdentifier Identifier { get; }
        public Credentials Credentials { get; private set; }
        public Password Password { get; private set; }
        public OrganizationIdentifier OrganizationIdentifier { get; }
    }
}