using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeManageData.Migrations
{
    public partial class Importance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeEstimation",
                table: "UserTasks",
                newName: "Importance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Importance",
                table: "UserTasks",
                newName: "TimeEstimation");
        }
    }
}
