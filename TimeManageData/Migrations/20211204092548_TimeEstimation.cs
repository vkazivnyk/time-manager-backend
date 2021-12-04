using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeManageData.Migrations
{
    public partial class TimeEstimation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalSeconds",
                table: "UserTasks",
                newName: "TimeEstimation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeEstimation",
                table: "UserTasks",
                newName: "TotalSeconds");
        }
    }
}
