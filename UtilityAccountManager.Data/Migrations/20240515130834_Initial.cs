using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtilityAccountManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    HouseNumber = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    FlatNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Zipcode = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Residents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UtilityAccounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    Area = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilityAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UtilityAccounts_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResidentUtilityAccounts",
                columns: table => new
                {
                    ResidentId = table.Column<int>(type: "INTEGER", nullable: false),
                    UtilityAccountNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentUtilityAccounts", x => new { x.ResidentId, x.UtilityAccountNumber });
                    table.ForeignKey(
                        name: "FK_ResidentUtilityAccounts_Residents_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "Residents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResidentUtilityAccounts_UtilityAccounts_UtilityAccountNumber",
                        column: x => x.UtilityAccountNumber,
                        principalTable: "UtilityAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResidentUtilityAccounts_UtilityAccountNumber",
                table: "ResidentUtilityAccounts",
                column: "UtilityAccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_UtilityAccounts_AddressId",
                table: "UtilityAccounts",
                column: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResidentUtilityAccounts");

            migrationBuilder.DropTable(
                name: "Residents");

            migrationBuilder.DropTable(
                name: "UtilityAccounts");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
