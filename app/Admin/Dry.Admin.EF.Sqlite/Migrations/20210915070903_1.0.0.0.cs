using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dry.Admin.EF.Sqlite.Migrations
{
    public partial class _1000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "系统id"),
                    Type = table.Column<int>(type: "INTEGER", nullable: false, comment: "类型"),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "名称"),
                    Secret = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "Secret"),
                    Url = table.Column<string>(type: "TEXT", nullable: true, comment: "地址"),
                    Description = table.Column<string>(type: "TEXT", nullable: true, comment: "说明"),
                    Enable = table.Column<bool>(type: "INTEGER", nullable: false, comment: "是否可用"),
                    AddTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "添加时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                },
                comment: "应用");

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false, comment: "系统id"),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true, comment: "上级资源id"),
                    AddTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "添加时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resource_Resource_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "系统资源");

            migrationBuilder.CreateTable(
                name: "ResourceAncestor",
                columns: table => new
                {
                    RelationId = table.Column<Guid>(type: "TEXT", nullable: false, comment: "系统资源id"),
                    AncestorId = table.Column<Guid>(type: "TEXT", nullable: false, comment: "祖先id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceAncestor", x => new { x.RelationId, x.AncestorId });
                    table.ForeignKey(
                        name: "FK_ResourceAncestor_Resource_AncestorId",
                        column: x => x.AncestorId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResourceAncestor_Resource_RelationId",
                        column: x => x.RelationId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "系统资源祖先关系");

            migrationBuilder.CreateTable(
                name: "ResourceItem",
                columns: table => new
                {
                    ResourceId = table.Column<Guid>(type: "TEXT", nullable: false, comment: "资源id"),
                    Type = table.Column<byte>(type: "INTEGER", nullable: false, comment: "资源项类型"),
                    AddTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "添加时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceItem", x => new { x.ResourceId, x.Type });
                    table.ForeignKey(
                        name: "FK_ResourceItem_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "资源项");

            migrationBuilder.CreateIndex(
                name: "IX_Application_AddTime",
                table: "Application",
                column: "AddTime");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_ParentId",
                table: "Resource",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceAncestor_AncestorId",
                table: "ResourceAncestor",
                column: "AncestorId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceItem_AddTime",
                table: "ResourceItem",
                column: "AddTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "ResourceAncestor");

            migrationBuilder.DropTable(
                name: "ResourceItem");

            migrationBuilder.DropTable(
                name: "Resource");
        }
    }
}
