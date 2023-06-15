using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoProf.Migrations
{
    /// <inheritdoc />
    public partial class TotalDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalDays",
                table: "Verlofs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AfdelingId",
                table: "Users",
                column: "AfdelingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_afdelingen_AfdelingId",
                table: "Users",
                column: "AfdelingId",
                principalTable: "afdelingen",
                principalColumn: "AfdelingId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_afdelingen_AfdelingId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AfdelingId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalDays",
                table: "Verlofs");
        }
    }
}
