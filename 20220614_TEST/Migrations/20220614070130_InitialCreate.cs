using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _20220614_TEST.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblActiveItem",
                columns: table => new
                {
                    cItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cActiveDt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblActiveItem", x => x.cItemID);
                });

            migrationBuilder.CreateTable(
                name: "tblSignup",
                columns: table => new
                {
                    cMobile = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    cName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    cEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    cCreateDT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSignup", x => x.cMobile);
                });

            migrationBuilder.CreateTable(
                name: "tblSignupItem",
                columns: table => new
                {
                    cMobile = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    cItemID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSignupItem", x => new { x.cItemID, x.cMobile });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblActiveItem");

            migrationBuilder.DropTable(
                name: "tblSignup");

            migrationBuilder.DropTable(
                name: "tblSignupItem");
        }
    }
}
