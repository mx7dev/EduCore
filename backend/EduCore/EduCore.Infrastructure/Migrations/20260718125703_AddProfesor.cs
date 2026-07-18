using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProfesor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dni = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Especialidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumeroCelular = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profesores_CorreoElectronico",
                table: "Profesores",
                column: "CorreoElectronico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profesores_Dni",
                table: "Profesores",
                column: "Dni",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profesores");
        }
    }
}
