using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetCoreLibrary.Domain.Organizations;
using NetCoreLibrary.Domain.Users;
using NetCoreLibrary.Infrastructure;
using NUnit.Framework;
using Shouldly;

namespace NetCoreLibrary.IntegrationTests.Infrastructure
{
    [TestFixture]
    public class UserSqlRepositoryTests
    {
        private Organization _knownOrganization;
        private Organization _unknownOrganization;
        
        [SetUp]
        public void Setup()
        {
            var organizationRepository = new OrganizationSqlRepository(CreateContext());
            _knownOrganization = new Organization(Guid.NewGuid().ToString());
            _unknownOrganization = new Organization(Guid.NewGuid().ToString());
            organizationRepository.Store(_knownOrganization);
            organizationRepository.Store(_unknownOrganization);
        }
        
        [Test]
        public async Task Store_returns_newly_created_organization()
        {
            // Arrange
            
            var sut = new UserSqlRepository(CreateContext());
            var user = new User(Guid.NewGuid() + "username", Guid.NewGuid() + "@email.com", "password", _knownOrganization.Identifier.Id);
            
            // Act

            await sut.Store(user);
            
            // Assert

            var reconstitutedUser = await sut.GetBy(user.Identifier);
            reconstitutedUser.Credentials.ShouldBe(user.Credentials);
            reconstitutedUser.Password.ShouldBe(user.Password);
            reconstitutedUser.OrganizationIdentifier.ShouldBe(user.OrganizationIdentifier);
        }
        
        [Test]
        public async Task Store_updates_existing_user()
        {
            // Arrange

            var updatedEmail = Guid.NewGuid() + "@email.com";
            var sut = new UserSqlRepository(CreateContext());
            var user = new User(Guid.NewGuid() + "username", Guid.NewGuid() + "@email.com", "password", _knownOrganization.Identifier.Id);
            await sut.Store(user);
            user.UpdateCredentials(user.Credentials.Username, updatedEmail);
            
            // Act

            await sut.Store(user);
            
            // Assert

            var reconstitutedUser = await sut.GetBy(user.Identifier);
            reconstitutedUser.Credentials.Email.ShouldBe(updatedEmail);
        }
        
        [Test]
        public async Task GetBy_returns_all_users_assigned_to_organization()
        {
            // Arrange

            var sut = new UserSqlRepository(CreateContext());
            var user1 = new User(Guid.NewGuid() + "username", Guid.NewGuid() + "@email.com", "password", _knownOrganization.Identifier.Id);
            var user2 = new User(Guid.NewGuid() + "username", Guid.NewGuid() + "@email.com", "password", _knownOrganization.Identifier.Id);
            var user3 = new User(Guid.NewGuid() + "username", Guid.NewGuid() + "@email.com", "password", _unknownOrganization.Identifier.Id);
            await sut.Store(user1);
            await sut.Store(user2);
            await sut.Store(user3);
            
            // Act

            var results = sut.GetBy(_knownOrganization.Identifier).ToList();
            
            // Assert

            results.Count.ShouldBe(2);
            results.Any(x => x.Identifier.Equals(user1.Identifier)).ShouldBeTrue();
            results.Any(x => x.Identifier.Equals(user2.Identifier)).ShouldBeTrue();
            results.Any(x => x.Identifier.Equals(user3.Identifier)).ShouldBeFalse();
        }

        private NetCoreLibraryDbContext CreateContext()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<NetCoreLibraryDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("NetCoreDatabase"));
            var context = new NetCoreLibraryDbContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}