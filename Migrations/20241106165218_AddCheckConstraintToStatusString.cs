using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckConstraintToStatusString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Pedidos",
                type: "longtext",
                nullable: false,
                defaultValue: "EmAndamento",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldDefaultValue: "EmAndamento")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
