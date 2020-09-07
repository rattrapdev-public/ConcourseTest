using System;
using Microsoft.EntityFrameworkCore;
using NetCoreLibrary.Domain.Users;

namespace NetCoreLibrary.Infrastructure
{
    public class NetCoreLibraryDbContext : DbContext
    {
        public NetCoreLibraryDbContext(DbContextOptions<NetCoreLibraryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrganizationDto>().HasData(
                new OrganizationDto { OrganizationId = new Guid("aac5a72a-2195-405e-b89c-01f3f227057b"), OrganizationName = "Acme Corp", IsEnabled = true},
                new OrganizationDto { OrganizationId = new Guid("7b29ba3c-48a2-44ab-a964-bffb0bccde0e"), OrganizationName = "Staten Island Textiles", IsEnabled = true},
                new OrganizationDto { OrganizationId = new Guid("8a056127-5fc7-459f-8649-64f53600dfbe"), OrganizationName = "Disabled Organization", IsEnabled = false}
            );

            modelBuilder.Entity<UserDto>().HasData(
                new UserDto{UserId = new Guid("83ac1fcb-e3a6-4ee9-a8cc-e72664974807"), Username="jdoe", Email = "jdoe@acme.com", HashedPassword = new Password("password").HashedPassword, OrganizationId = new Guid("aac5a72a-2195-405e-b89c-01f3f227057b")},
                new UserDto{UserId = new Guid("c485ca29-9b4f-46a8-89af-33834b0f52ec"), Username="mwinger", Email = "matt.winger@acme.com", HashedPassword = new Password("password").HashedPassword, OrganizationId = new Guid("aac5a72a-2195-405e-b89c-01f3f227057b")},
                new UserDto{UserId = new Guid("d0fbfd82-493e-4be1-95d9-516c2116ec74"), Username="psmith", Email = "pete@sit.com", HashedPassword = new Password("password").HashedPassword, OrganizationId = new Guid("7b29ba3c-48a2-44ab-a964-bffb0bccde0e")},
                new UserDto{UserId = new Guid("8241df34-f3b6-4950-a933-c3d6978db2ed"), Username="unknown1", Email = "unknown1@disabled.com", HashedPassword = new Password("password").HashedPassword, OrganizationId = new Guid("8a056127-5fc7-459f-8649-64f53600dfbe")},
                new UserDto{UserId = new Guid("98974a90-d787-4e60-8656-ac3863223c30"), Username="unknown2", Email = "unknown2@disabled.com", HashedPassword = new Password("password").HashedPassword, OrganizationId = new Guid("8a056127-5fc7-459f-8649-64f53600dfbe")}
            );
        }

        public DbSet<OrganizationDto> Organizations { get; set; }
        public DbSet<UserDto> Users { get; set; }
    }
}