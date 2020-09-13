﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetCoreLibrary.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NetCoreLibrary.Migrations
{
    [DbContext(typeof(NetCoreLibraryDbContext))]
    partial class NetCoreLibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("NetCoreLibrary.Infrastructure.OrganizationDto", b =>
                {
                    b.Property<Guid>("OrganizationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("OrganizationName")
                        .HasColumnType("text");

                    b.HasKey("OrganizationId");

                    b.ToTable("organization");

                    b.HasData(
                        new
                        {
                            OrganizationId = new Guid("aac5a72a-2195-405e-b89c-01f3f227057b"),
                            IsEnabled = true,
                            OrganizationName = "Acme Corp"
                        },
                        new
                        {
                            OrganizationId = new Guid("7b29ba3c-48a2-44ab-a964-bffb0bccde0e"),
                            IsEnabled = true,
                            OrganizationName = "Staten Island Textiles"
                        },
                        new
                        {
                            OrganizationId = new Guid("8a056127-5fc7-459f-8649-64f53600dfbe"),
                            IsEnabled = false,
                            OrganizationName = "Disabled Organization"
                        });
                });

            modelBuilder.Entity("NetCoreLibrary.Infrastructure.UserDto", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("text");

                    b.Property<Guid?>("OrganizationDtoOrganizationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("OrganizationDtoOrganizationId");

                    b.ToTable("user");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("83ac1fcb-e3a6-4ee9-a8cc-e72664974807"),
                            Email = "jdoe@acme.com",
                            HashedPassword = "5F4DCC3B5AA765D61D8327DEB882CF99",
                            OrganizationId = new Guid("aac5a72a-2195-405e-b89c-01f3f227057b"),
                            Username = "jdoe"
                        },
                        new
                        {
                            UserId = new Guid("c485ca29-9b4f-46a8-89af-33834b0f52ec"),
                            Email = "matt.winger@acme.com",
                            HashedPassword = "5F4DCC3B5AA765D61D8327DEB882CF99",
                            OrganizationId = new Guid("aac5a72a-2195-405e-b89c-01f3f227057b"),
                            Username = "mwinger"
                        },
                        new
                        {
                            UserId = new Guid("d0fbfd82-493e-4be1-95d9-516c2116ec74"),
                            Email = "pete@sit.com",
                            HashedPassword = "5F4DCC3B5AA765D61D8327DEB882CF99",
                            OrganizationId = new Guid("7b29ba3c-48a2-44ab-a964-bffb0bccde0e"),
                            Username = "psmith"
                        },
                        new
                        {
                            UserId = new Guid("8241df34-f3b6-4950-a933-c3d6978db2ed"),
                            Email = "unknown1@disabled.com",
                            HashedPassword = "5F4DCC3B5AA765D61D8327DEB882CF99",
                            OrganizationId = new Guid("8a056127-5fc7-459f-8649-64f53600dfbe"),
                            Username = "unknown1"
                        },
                        new
                        {
                            UserId = new Guid("98974a90-d787-4e60-8656-ac3863223c30"),
                            Email = "unknown2@disabled.com",
                            HashedPassword = "5F4DCC3B5AA765D61D8327DEB882CF99",
                            OrganizationId = new Guid("8a056127-5fc7-459f-8649-64f53600dfbe"),
                            Username = "unknown2"
                        });
                });

            modelBuilder.Entity("NetCoreLibrary.Infrastructure.UserDto", b =>
                {
                    b.HasOne("NetCoreLibrary.Infrastructure.OrganizationDto", null)
                        .WithMany("Users")
                        .HasForeignKey("OrganizationDtoOrganizationId");
                });
#pragma warning restore 612, 618
        }
    }
}
