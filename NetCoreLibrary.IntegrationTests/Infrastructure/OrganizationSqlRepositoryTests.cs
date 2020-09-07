using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetCoreLibrary.Domain.Organizations;
using NetCoreLibrary.Infrastructure;
using NUnit.Framework;
using Shouldly;

namespace NetCoreLibrary.IntegrationTests.Infrastructure
{
    [TestFixture]
    public class OrganizationSqlRepositoryTests
    {
        private NetCoreLibraryDbContext _context;
        
        [SetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<NetCoreLibraryDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("NetCoreDatabase"));
            _context = new NetCoreLibraryDbContext(optionsBuilder.Options);
            _context.Database.EnsureCreated();
        }
        
        [Test]
        public async Task Store_returns_newly_created_organization()
        {
            // Arrange
            
            var sut = new OrganizationSqlRepository(_context);
            var organization = new Organization(Guid.NewGuid().ToString());
            
            // Act

            await sut.Store(organization);
            
            // Assert

            var reconstitutedOrganization = await sut.GetBy(organization.Identifier);
            reconstitutedOrganization.Name.ShouldBe(organization.Name);
            reconstitutedOrganization.IsEnabled.ShouldBe(organization.IsEnabled);
        }
        
        [Test]
        public async Task Store_updates_existing_organization()
        {
            // Arrange

            var newName = Guid.NewGuid().ToString();
            var sut = new OrganizationSqlRepository(_context);
            var organization = new Organization(Guid.NewGuid().ToString());
            await sut.Store(organization);
            organization.UpdateName(newName);
            
            // Act

            await sut.Store(organization);
            
            // Assert

            var reconstitutedOrganization = await sut.GetBy(organization.Identifier);
            reconstitutedOrganization.Name.Name.ShouldBe(newName);
        }
        
        [Test]
        public async Task SearchBy_returns_all_organizations_matching_name()
        {
            // Arrange

            var uniqueText = Guid.NewGuid().ToString();
            var sut = new OrganizationSqlRepository(_context);
            var organization1 = new Organization(Guid.NewGuid().ToString() + uniqueText);
            var organization2 = new Organization(Guid.NewGuid().ToString() + uniqueText);
            await sut.Store(organization1);
            await sut.Store(organization2);
            
            // Act

            var results = sut.SearchBy(uniqueText).ToList();
            
            // Assert

            results.Count.ShouldBe(2);
            results.Any(x => x.Identifier.Equals(organization1.Identifier)).ShouldBeTrue();
            results.Any(x => x.Identifier.Equals(organization2.Identifier)).ShouldBeTrue();
        }
    }
}