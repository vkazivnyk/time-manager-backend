using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeManageData.Migrations
{
    public partial class TotalSecondsDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalSeconds",
                table: "UserTasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveTimeEnd",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveTimeStart",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveTimeEnd",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ActiveTimeStart",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<long>(
                name: "TotalSeconds",
                table: "UserTasks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
