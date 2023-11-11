using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student.DAL.Migrations
{
    /// <inheritdoc />
    public partial class onDelete_On_Class_And_Nullable_Foreign_Key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_Users_UserId",
                table: "Class");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Class",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_Users_UserId",
                table: "Class",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_Users_UserId",
                table: "Class");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Class",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Class_Users_UserId",
                table: "Class",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
