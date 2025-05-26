using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonar.MVC.Migrations
{
    /// <inheritdoc />
    public partial class DateTimeandotherFieldaremdified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Bloodgroup",
                table: "BloodDonors",
                newName: "BloodGroup");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "BloodDonors",
                newName: "DateOFBirth");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastDonationDate",
                table: "BloodDonors",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BloodGroup",
                table: "BloodDonors",
                newName: "Bloodgroup");

            migrationBuilder.RenameColumn(
                name: "DateOFBirth",
                table: "BloodDonors",
                newName: "DateTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastDonationDate",
                table: "BloodDonors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
