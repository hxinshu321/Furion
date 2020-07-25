﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fur.EntityFramework.Core.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "CreatedTime", "Host", "IsDeleted", "Name", "UpdatedTime" },
                values: new object[] { 1, new DateTime(2020, 7, 25, 23, 25, 16, 210, DateTimeKind.Local).AddTicks(9389), "localhost:44307", false, "默认租户", new DateTime(2020, 7, 25, 23, 25, 16, 211, DateTimeKind.Local).AddTicks(9552) });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "CreatedTime", "Host", "IsDeleted", "Name", "UpdatedTime" },
                values: new object[] { 2, new DateTime(2020, 7, 25, 23, 25, 16, 212, DateTimeKind.Local).AddTicks(1713), "localhost:41529", false, "默认租户", new DateTime(2020, 7, 25, 23, 25, 16, 212, DateTimeKind.Local).AddTicks(1719) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Tests");
        }
    }
}