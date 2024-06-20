using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class _1900624v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    ArticuloId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Codigo = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Precio = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.ArticuloId);
                });

            migrationBuilder.CreateTable(
                name: "Encargados",
                columns: table => new
                {
                    EncargadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encargados", x => x.EncargadoId);
                });

            migrationBuilder.CreateTable(
                name: "TiposMovimiento",
                columns: table => new
                {
                    TipoMovimientoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EsAumento = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposMovimiento", x => x.TipoMovimientoId);
                });

            migrationBuilder.CreateTable(
                name: "StockArticulos",
                columns: table => new
                {
                    MovimientoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    TipoMovimientoId = table.Column<int>(type: "int", nullable: false),
                    EmailEncargado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadMovida = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockArticulos", x => x.MovimientoId);
                    table.ForeignKey(
                        name: "FK_StockArticulos_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "ArticuloId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockArticulos_TiposMovimiento_TipoMovimientoId",
                        column: x => x.TipoMovimientoId,
                        principalTable: "TiposMovimiento",
                        principalColumn: "TipoMovimientoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_Codigo",
                table: "Articulos",
                column: "Codigo",
                unique: true,
                filter: "[Codigo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_Nombre",
                table: "Articulos",
                column: "Nombre",
                unique: true,
                filter: "[Nombre] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Encargados_Email",
                table: "Encargados",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockArticulos_ArticuloId",
                table: "StockArticulos",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_StockArticulos_TipoMovimientoId",
                table: "StockArticulos",
                column: "TipoMovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposMovimiento_Nombre",
                table: "TiposMovimiento",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Encargados");

            migrationBuilder.DropTable(
                name: "StockArticulos");

            migrationBuilder.DropTable(
                name: "Articulos");

            migrationBuilder.DropTable(
                name: "TiposMovimiento");
        }
    }
}
