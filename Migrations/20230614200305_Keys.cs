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
            migrationBuilder.DropForeignKey(
                name: "FK_Users_afdelingen_AfdelingId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "AfdelingId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_afdelingen_AfdelingId",
                table: "Users",
                column: "AfdelingId",
                principalTable: "afdelingen",
                principalColumn: "AfdelingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_afdelingen_AfdelingId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "AfdelingId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_afdelingen_AfdelingId",
                table: "Users",
                column: "AfdelingId",
                principalTable: "afdelingen",
                principalColumn: "AfdelingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
