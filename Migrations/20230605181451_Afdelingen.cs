using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoProf.Migrations
{
    /// <inheritdoc />
    public partial class Afdelingen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "afdelingen",
                columns: table => new
                {
                    AfdelingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AfdelingNaam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_afdelingen", x => x.AfdelingId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "afdelingen");
        }
    }
}
