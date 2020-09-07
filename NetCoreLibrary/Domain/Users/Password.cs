using System;
using System.Security.Cryptography;
using System.Text;

namespace NetCoreLibrary.Domain.Users
{
    public class Password : IEquatable<Password>
    {
        public Password(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password is required");
            }

            if (password.Length < 8)
            {
                throw new ArgumentException("Password must be at least 8 characters");
            }

            HashedPassword = GetHashString(password);
        }

        public Password(string hashedPassword, bool extra)
        {
            HashedPassword = hashedPassword;
        }
        
        public string HashedPassword { get; }
        
        private static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public bool Equals(Password other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return HashedPassword == other.HashedPassword;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Password) obj);
        }

        public override int GetHashCode()
        {
            return (HashedPassword != null ? HashedPassword.GetHashCode() : 0);
        }

        public static bool operator ==(Password left, Password right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Password left, Password right)
        {
            return !Equals(left, right);
        }
    }
}