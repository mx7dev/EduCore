using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameAnioField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Año",
                table: "Periodos",
                newName: "Anio");

            migrationBuilder.RenameIndex(
                name: "IX_Periodos_Año",
                table: "Periodos",
                newName: "IX_Periodos_Anio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Anio",
                table: "Periodos",
                newName: "Año");

            migrationBuilder.RenameIndex(
                name: "IX_Periodos_Anio",
                table: "Periodos",
                newName: "IX_Periodos_Año");
        }
    }
}
