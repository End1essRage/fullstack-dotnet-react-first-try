using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodosBackend.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "todos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_todos_user_id",
                table: "todos",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_todos_users_user_id",
                table: "todos",
                column: "user_id",
                principalTable: "users",
                principalColumn: "_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todos_users_user_id",
                table: "todos");

            migrationBuilder.DropIndex(
                name: "IX_todos_user_id",
                table: "todos");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "todos");
        }
    }
}
