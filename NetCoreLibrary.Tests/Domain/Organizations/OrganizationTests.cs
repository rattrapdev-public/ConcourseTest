using System;
using NetCoreLibrary.Domain.Organizations;
using NUnit.Framework;
using Shouldly;

namespace NetCoreLibrary.Tests.Domain.Organizations
{
    [TestFixture]
    public class OrganizationTests
    {
        [Test]
        public void Constructor_sets_initial_values()
        {
            // Arrange

            var name = Guid.NewGuid().ToString();
            
            // Act
            
            var result = new Organization(name);
            
            // Assert
            
            result.Identifier.Id.ShouldNotBe(Guid.Empty);
            result.Name.Name.ShouldBe(name);
            result.IsEnabled.ShouldBeTrue();
        }

        [Test]
        public void Constructor_reconstitutes_Organization()
        {
            // Arrange
            
            var organizationIdentifier = new OrganizationIdentifier();
            var organizationName = new OrganizationName(Guid.NewGuid().ToString());
            var isEnabled = false;
            
            // Act
            
            var result = new Organization(organizationIdentifier.Id, organizationName.Name, isEnabled);
            
            // Assert
            
            result.Identifier.ShouldBe(organizationIdentifier);
            result.Name.ShouldBe(organizationName);
            result.IsEnabled.ShouldBe(isEnabled);
        }

        [Test]
        public void UpdateName_sets_name_to_new_value()
        {
            // Arrange
            
            var sut = new Organization(Guid.NewGuid().ToString());
            var updatedName = Guid.NewGuid().ToString();
            
            // Act
            
            sut.UpdateName(updatedName);
            
            // Assert
            
            sut.Name.Name.ShouldBe(updatedName);
        }

        [Test]
        public void Disable_sets_isEnabled_to_false()
        {
            // Arrange
            
            var sut = new Organization(Guid.NewGuid().ToString());
            
            // Act
            
            sut.Disable();
            
            // Assert
            
            sut.IsEnabled.ShouldBeFalse();
        }

        [Test]
        public void Enable_sets_isEnabled_to_true()
        {
            // Arrange
            
            var sut = new Organization(Guid.NewGuid(), Guid.NewGuid().ToString(), false);
            
            // Act
            
            sut.Enable();
            
            // Assert
            
            sut.IsEnabled.ShouldBeTrue();
        }
    }
}