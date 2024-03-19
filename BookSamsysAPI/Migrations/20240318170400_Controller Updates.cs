using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSamsysAPI.Migrations
{
    /// <inheritdoc />
    public partial class ControllerUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Books",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Books",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ISBN",
                table: "Books",
                newName: "iSBN");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Books",
                newName: "author");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Books",
                newName: "id");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "Books",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Books",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Books",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "iSBN",
                table: "Books",
                newName: "ISBN");

            migrationBuilder.RenameColumn(
                name: "author",
                table: "Books",
                newName: "Author");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Books",
                newName: "Id");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Books",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
