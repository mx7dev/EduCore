using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Secciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Turno = table.Column<int>(type: "int", nullable: false),
                    GradoId = table.Column<int>(type: "int", nullable: false),
                    PeriodoId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Secciones_Grados_GradoId",
                        column: x => x.GradoId,
                        principalTable: "Grados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Secciones_Periodos_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "Periodos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_GradoId",
                table: "Secciones",
                column: "GradoId");

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_Nombre_GradoId_PeriodoId",
                table: "Secciones",
                columns: new[] { "Nombre", "GradoId", "PeriodoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_PeriodoId",
                table: "Secciones",
                column: "PeriodoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Secciones");
        }
    }
}
