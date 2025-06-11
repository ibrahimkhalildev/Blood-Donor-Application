using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloodDonar.MVC.Migrations
{
    /// <inheritdoc />
    public partial class BloodDonorDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodDonors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOFBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodGroup = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastDonationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodDonors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HospitalName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BloodDonationId = table.Column<int>(type: "int", nullable: false),
                    BloodDonorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_BloodDonors_BloodDonorId",
                        column: x => x.BloodDonorId,
                        principalTable: "BloodDonors",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "BloodDonors",
                columns: new[] { "Id", "Address", "BloodGroup", "ContactNumber", "DateOFBirth", "Email", "LastDonationDate", "Name", "ProfilePicture", "Weight" },
                values: new object[,]
                {
                    { 1, "123 Main St, Dhaka", 6, "01932878112", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mahbuburrahman@example.com", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mahbubur Rahman", "profiles/mahbubur.jpg", 70.5f },
                    { 2, "9/A West Paikpara, Dhaka", 6, "01582878199", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IbrahimKhalil@example.com", new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ibrahim Khalil", "profiles/ibrahim.jpg", 70.5f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_BloodDonorId",
                table: "Donations",
                column: "BloodDonorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "BloodDonors");
        }
    }
}
