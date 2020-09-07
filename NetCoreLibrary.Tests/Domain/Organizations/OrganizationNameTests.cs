using System;
using NetCoreLibrary.Domain.Organizations;
using NUnit.Framework;
using Shouldly;

namespace NetCoreLibrary.Tests.Domain.Organizations
{
    [TestFixture]
    public class OrganizationNameTests
    {
        [Test]
        public void Constructor_sets_name()
        {
            // Arrange

            var name = Guid.NewGuid().ToString();
            
            // Act
            
            var result = new OrganizationName(name);
            
            // Assert
            
            result.Name.ShouldBe(name);
        }

        [Test]
        public void Constructor_throws_with_empty_name()
        {
            // Act / Assert

            Should.Throw<ArgumentException>(() => new OrganizationName(String.Empty));
        }

        [Test]
        public void Equals_returns_true_for_same_value()
        {
            // Arrange 

            var name = Guid.NewGuid().ToString();
            var sut = new OrganizationName(name);
            var other = new OrganizationName(name);
            
            // Act

            var result = sut.Equals(other);
            
            // Assert
            
            result.ShouldBeTrue();
        }

        [Test]
        public void Equals_returns_false_for_different_values()
        {
            // Arrange
            
            var sut = new OrganizationName(Guid.NewGuid().ToString());
            var other = new OrganizationName(Guid.NewGuid().ToString());
            
            // Act

            var result = sut.Equals(other);
            
            // Assert
            
            result.ShouldBeFalse();
        }
    }
}