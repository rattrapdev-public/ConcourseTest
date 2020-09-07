using System;
using System.Net.Mail;

namespace NetCoreLibrary.Domain.Users
{
    public class Credentials : IEquatable<Credentials>
    {
        public Credentials(string username, string email)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username must be a non-empty string");
            }

            try
            {
                new MailAddress(email);
            }
            catch (Exception)
            {
                throw new ArgumentException("Email must be a valid email address");
            }

            Username = username;
            Email = email;
        }
        
        public string Username { get; }
        public string Email { get; }

        public bool Equals(Credentials other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Username == other.Username && Email == other.Email;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Credentials) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Username, Email);
        }

        public static bool operator ==(Credentials left, Credentials right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Credentials left, Credentials right)
        {
            return !Equals(left, right);
        }
    }
}