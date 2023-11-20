using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dry.Admin.EF.Sqlite.Migrations
{
    public partial class _1002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Resource",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Resource");
        }
    }
}
