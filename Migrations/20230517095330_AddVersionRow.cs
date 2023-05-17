using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesMovie.Migrations
{
    /// <inheritdoc />
    public partial class AddVersionRow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Home_Director_DirectorID",
                table: "Home");

            migrationBuilder.AddColumn<Guid>(
                name: "ConcurrencyToken",
                table: "Studio",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "DirectorID",
                table: "Home",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Home_Director_DirectorID",
                table: "Home",
                column: "DirectorID",
                principalTable: "Director",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Home_Director_DirectorID",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "ConcurrencyToken",
                table: "Studio");

            migrationBuilder.AlterColumn<int>(
                name: "DirectorID",
                table: "Home",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Home_Director_DirectorID",
                table: "Home",
                column: "DirectorID",
                principalTable: "Director",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
