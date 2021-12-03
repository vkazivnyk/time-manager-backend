using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeManageData.Migrations
{
    public partial class ActiveTimeSeconds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveTimeEnd",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ActiveTimeStart",
                table: "AspNetUsers");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveTimeEndSeconds",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ActiveTimeStartSeconds",
                table: "AspNetUsers");

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
    }
}
