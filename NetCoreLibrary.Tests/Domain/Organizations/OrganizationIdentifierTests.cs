using System;
using NetCoreLibrary.Domain.Organizations;
using NUnit.Framework;
using Shouldly;

namespace NetCoreLibrary.Tests.Domain.Organizations
{
    [TestFixture]
    public class OrganizationIdentifierTests
    {
        [Test]
        public void Constructor_sets_new_id()
        {
            // Arrange / Act

            var result = new OrganizationIdentifier();
            
            // Assert
            
            result.Id.ShouldNotBe(Guid.Empty);
        }

        [Test]
        public void Constructor_sets_id()
        {
            // Arrange

            var id = Guid.NewGuid();
            
            // Act
            
            var result = new OrganizationIdentifier(id);
            
            // Assert
            
            result.Id.ShouldBe(id);
        }

        [Test]
        public void Constructor_throws_with_empty_id()
        {
            // Act / Assert

            Should.Throw<ArgumentException>(() => new OrganizationIdentifier(Guid.Empty));
        }

        [Test]
        public void Equals_returns_true_for_same_value()
        {
            // Arrange 

            var id = Guid.NewGuid();
            var sut = new OrganizationIdentifier(id);
            var other = new OrganizationIdentifier(id);
            
            // Act

            var result = sut.Equals(other);
            
            // Assert
            
            result.ShouldBeTrue();
        }

        [Test]
        public void Equals_returns_false_for_different_values()
        {
            // Arrange
            
            var sut = new OrganizationIdentifier(Guid.NewGuid());
            var other = new OrganizationIdentifier(Guid.NewGuid());
            
            // Act

            var result = sut.Equals(other);
            
            // Assert
            
            result.ShouldBeFalse();
        }
    }
}