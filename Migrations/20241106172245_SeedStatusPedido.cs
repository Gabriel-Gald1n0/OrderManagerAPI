using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedStatusPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "StatusPedidoId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StatusPedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPedidos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "StatusPedidos",
                columns: new[] { "Id", "Status" },
                values: new object[,]
                {
                    { 1, "Concluido" },
                    { 2, "Em andamento" },
                    { 3, "Cancelado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_StatusPedidoId",
                table: "Pedidos",
                column: "StatusPedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_StatusPedidos_StatusPedidoId",
                table: "Pedidos",
                column: "StatusPedidoId",
                principalTable: "StatusPedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_StatusPedidos_StatusPedidoId",
                table: "Pedidos");

            migrationBuilder.DropTable(
                name: "StatusPedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_StatusPedidoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "StatusPedidoId",
                table: "Pedidos");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Pedidos",
                type: "longtext",
                nullable: false,
                defaultValue: "EmAndamento")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
