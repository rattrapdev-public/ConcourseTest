using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetCoreLibrary.Infrastructure;
using NUnit.Framework;
using Shouldly;

namespace NetCoreLibrary.IntegrationTests.Infrastructure
{
    [TestFixture]
    public class NetCoreLibraryDbContextTests
    {
        public IConfigurationRoot Configuration { get; set; }
        [Test]
        public void TestConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<NetCoreLibraryDbContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("NetCoreDatabase"));
            var dbContext = new NetCoreLibraryDbContext(optionsBuilder.Options);
            dbContext.Database.EnsureCreated();

            var x = dbContext.Organizations.Add(new OrganizationDto
            {
                OrganizationId = Guid.NewGuid(), OrganizationName = Guid.NewGuid().ToString() + "Organization",
                IsEnabled = true
            });

            dbContext.SaveChanges();

            var y = dbContext.Organizations.First(g => g.OrganizationName.Contains("Organization"));
            y.ShouldNotBeNull();
        }
    }
}