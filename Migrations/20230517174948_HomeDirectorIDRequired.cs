using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesMovie.Migrations
{
    /// <inheritdoc />
    public partial class HomeDirectorIDRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Home_Director_DirectorID",
                table: "Home");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Home_Director_DirectorID",
                table: "Home");

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
    }
}
