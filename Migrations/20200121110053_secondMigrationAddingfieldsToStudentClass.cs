using Microsoft.EntityFrameworkCore.Migrations;

namespace Tp3_MVC.Migrations
{
    public partial class secondMigrationAddingfieldsToStudentClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fullName",
                table: "Students",
                newName: "lastName");

            migrationBuilder.AddColumn<string>(
                name: "firstName",
                table: "Students",
                type: "VARCHAR(30)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "firstName",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Students",
                newName: "fullName");
        }
    }
}
