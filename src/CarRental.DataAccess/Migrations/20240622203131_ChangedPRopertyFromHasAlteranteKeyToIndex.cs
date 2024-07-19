using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPRopertyFromHasAlteranteKeyToIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Vehicles_PlateNumber",
                table: "Vehicles");

            migrationBuilder.AlterDatabase(
                collation: "Finnish_Swedish_100_CI_AS");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_PlateNumber",
                table: "Vehicles",
                column: "PlateNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_PlateNumber",
                table: "Vehicles");

            migrationBuilder.AlterDatabase(
                oldCollation: "Finnish_Swedish_100_CI_AS");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Vehicles_PlateNumber",
                table: "Vehicles",
                column: "PlateNumber");
        }
    }
}
