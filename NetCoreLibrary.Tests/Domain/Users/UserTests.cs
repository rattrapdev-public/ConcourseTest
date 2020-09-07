using System;
using NetCoreLibrary.Domain.Users;
using NUnit.Framework;
using Shouldly;

namespace NetCoreLibrary.Tests.Domain.Users
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void Constructor_sets_initial_values()
        {
            // Arrange

            var organizationId = Guid.NewGuid();
            
            // Act
            
            var result = new User("username", "email@email.com", "password", organizationId);
            
            // Assert
            
            result.Identifier.Id.ShouldNotBe(Guid.Empty);
            result.Credentials.Username.ShouldBe("username");
            result.Credentials.Email.ShouldBe("email@email.com");
            result.Password.ShouldBe(new Password("password"));
            result.OrganizationIdentifier.Id.ShouldBe(organizationId);
        }

        [Test]
        public void ResetPassword_throws_with_different_old_password()
        {
            // Arrange

            var organizationId = Guid.NewGuid();
            var sut = new User("username", "email@email.com", "password", organizationId);
            
            // Act / Assert
            
            Should.Throw<ArgumentException>(() => sut.ResetPassword("password2", "password3"));
        }

        [Test]
        public void ResetPassword_updates_user_to_new_password()
        {
            // Arrange

            var organizationId = Guid.NewGuid();
            var sut = new User("username", "email@email.com", "password", organizationId);
            
            // Act
            
            sut.ResetPassword("password", "password2");
            
            // Assert
            
            sut.Password.ShouldBe(new Password("password2"));
        }
    }
}