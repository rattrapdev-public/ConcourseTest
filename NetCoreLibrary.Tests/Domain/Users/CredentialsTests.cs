using System;
using NetCoreLibrary.Domain.Users;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace NetCoreLibrary.Tests.Domain.Users
{
    [TestFixture]
    public class CredentialsTests
    {
        [Test]
        public void Constructor_sets_values()
        {
            // Arrange

            var username = Guid.NewGuid().ToString();
            var email = Guid.NewGuid().ToString() + "@email.com";
            
            // Act
            
            var result = new Credentials(username, email);
            
            // Assert
            
            result.Username.ShouldBe(username);
            result.Email.ShouldBe(email);
        }

        [Test]
        public void Constructor_throws_with_empty_username()
        {
            // Act / Assert

            Should.Throw<ArgumentException>(() => new Credentials(string.Empty, "test@test.com"));
        }

        [Test]
        public void Constructor_throws_with_invalid_email()
        {
            // Act / Assert

            Should.Throw<ArgumentException>(() => new Credentials("name", string.Empty));
        }
    }
}