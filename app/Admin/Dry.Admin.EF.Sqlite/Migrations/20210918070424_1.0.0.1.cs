using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dry.Admin.EF.Sqlite.Migrations
{
    public partial class _1001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Application",
                type: "INTEGER",
                nullable: false,
                comment: "类型（1：Mvc系统，2：前端系统，3：Web接口，4：Grpc服务，5：Windows服务，6：Android应用，7：Apple应用）",
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldComment: "类型");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "Application",
                type: "TEXT",
                nullable: true,
                comment: "更新时间");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "Application");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Application",
                type: "INTEGER",
                nullable: false,
                comment: "类型",
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldComment: "类型（1：Mvc系统，2：前端系统，3：Web接口，4：Grpc服务，5：Windows服务，6：Android应用，7：Apple应用）");
        }
    }
}
