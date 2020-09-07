using System;
using NetCoreLibrary.Domain.Users;
using NUnit.Framework;
using Shouldly;

namespace NetCoreLibrary.Tests.Domain.Users
{
    [TestFixture]
    public class PasswordTests
    {
        [Test]
        public void Constructor_sets_value()
        {
            // Act
            
            var result = new Password("12345678");
            
            // Assert

            result.HashedPassword.ShouldNotBe("");
        }

        [Test]
        public void Constructor_throws_with_short_password()
        {
            // Act / Assert

            Should.Throw<ArgumentException>(() => new Password("1234567"));
        }
    }
}