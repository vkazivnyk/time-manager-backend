using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeManageData.Migrations
{
    public partial class RemoveActiveTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveTimeEndSeconds",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ActiveTimeStartSeconds",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActiveTimeEndSeconds",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActiveTimeStartSeconds",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
