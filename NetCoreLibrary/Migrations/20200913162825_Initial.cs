using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreLibrary.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organization",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    OrganizationName = table.Column<string>(nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    HashedPassword = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    OrganizationDtoOrganizationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_user_organization_OrganizationDtoOrganizationId",
                        column: x => x.OrganizationDtoOrganizationId,
                        principalTable: "organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "organization",
                columns: new[] { "OrganizationId", "IsEnabled", "OrganizationName" },
                values: new object[,]
                {
                    { new Guid("aac5a72a-2195-405e-b89c-01f3f227057b"), true, "Acme Corp" },
                    { new Guid("7b29ba3c-48a2-44ab-a964-bffb0bccde0e"), true, "Staten Island Textiles" },
                    { new Guid("8a056127-5fc7-459f-8649-64f53600dfbe"), false, "Disabled Organization" }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "UserId", "Email", "HashedPassword", "OrganizationDtoOrganizationId", "OrganizationId", "Username" },
                values: new object[,]
                {
                    { new Guid("83ac1fcb-e3a6-4ee9-a8cc-e72664974807"), "jdoe@acme.com", "5F4DCC3B5AA765D61D8327DEB882CF99", null, new Guid("aac5a72a-2195-405e-b89c-01f3f227057b"), "jdoe" },
                    { new Guid("c485ca29-9b4f-46a8-89af-33834b0f52ec"), "matt.winger@acme.com", "5F4DCC3B5AA765D61D8327DEB882CF99", null, new Guid("aac5a72a-2195-405e-b89c-01f3f227057b"), "mwinger" },
                    { new Guid("d0fbfd82-493e-4be1-95d9-516c2116ec74"), "pete@sit.com", "5F4DCC3B5AA765D61D8327DEB882CF99", null, new Guid("7b29ba3c-48a2-44ab-a964-bffb0bccde0e"), "psmith" },
                    { new Guid("8241df34-f3b6-4950-a933-c3d6978db2ed"), "unknown1@disabled.com", "5F4DCC3B5AA765D61D8327DEB882CF99", null, new Guid("8a056127-5fc7-459f-8649-64f53600dfbe"), "unknown1" },
                    { new Guid("98974a90-d787-4e60-8656-ac3863223c30"), "unknown2@disabled.com", "5F4DCC3B5AA765D61D8327DEB882CF99", null, new Guid("8a056127-5fc7-459f-8649-64f53600dfbe"), "unknown2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_OrganizationDtoOrganizationId",
                table: "user",
                column: "OrganizationDtoOrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "organization");
        }
    }
}
