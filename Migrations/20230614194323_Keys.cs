using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoProf.Migrations
{
    /// <inheritdoc />
    public partial class Keys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_AfdelingId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AfdelingId",
                table: "Users",
                column: "AfdelingId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_AfdelingId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AfdelingId",
                table: "Users",
                column: "AfdelingId");
        }
    }
}
